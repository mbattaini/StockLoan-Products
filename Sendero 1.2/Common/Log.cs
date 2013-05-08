// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using System.IO;
using System.Diagnostics;

namespace Anetics.Common
{
    public class Log
    {
        public const string Error = "";
        public const string FailureAudit = "";
        public const string Information = "";
        public const string SuccessAudit = "";
        public const string Warning = "";

        private static byte level = 1;
        private static string filePath = "";
        private static string name = Standard.Developer;

        /// <summary>
        /// Threshold at or after which an event is to be logged.
        /// </summary>
        public static string Level
        {
            set
            {
                try
                {
                    level = byte.Parse(value);
                }
                catch { }
            }

            get
            {
                return level.ToString();
            }
        }

        /// <summary>
        /// Directory in which log file is to be written.
        /// </summary>
        public static string FilePath
        {
            set
            {
                if (value.EndsWith(@"\"))
                {
                    filePath = value;
                }
                else
                {
                    filePath = value + @"\";
                }
            }

            get
            {
                return filePath;
            }
        }

        /// <summary>
        /// Name for the log.
        /// </summary>
        public static string Name
        {
            set
            {
                if (value.Equals(""))
                {
                    name = Standard.Developer;
                }
                else
                {
                    name = value;
                }
            }

            get
            {
                return name;
            }
        }

        /// <summary>
        /// Makes a log file entry if log level is at or above threshold log level.
        /// </summary>
        public static void Write(string eventText, byte logLevel)
        {
            if (logLevel <= level)
            {
                WriteFile(eventText);
            }
        }

        /// <summary>
        /// Makes an Event Viewer Application entry.
        /// </summary>
        public static void Write(string eventText, string entryType)
        {
            WriteEvent(eventText, entryType);
        }

        /// <summary>
        /// Makes both a log file and Event Viewer entry if log level is at or above threshold log level.
        /// </summary>
        public static void Write(string eventText, string entryType, byte logLevel)
        {
            if (logLevel <= level)
            {
                WriteFile(eventText);
                Write(eventText, entryType);
            }
        }

        /// <summary>
        /// Writes event text to a log file.
        /// </summary>
        private static void WriteFile(string eventText)
        {
            if (filePath.Equals("")) // Path not specified for log file.
            {
                return;
            }

            DateTime utcDateTimeNow = DateTime.UtcNow;
            string timeStamp = utcDateTimeNow.ToString("HH:mm:ss.fff");

            lock (typeof(Log)) // Defend against file write access concurrency.
            {
                try
                {
                    string filePathName = filePath + name + "." + utcDateTimeNow.ToString("yyyyMMdd") + ".log";
                    StreamWriter streamWriter = new StreamWriter(filePathName, true);

                    if (eventText.Length > 0) // Add the time stamp.
                    {
                        streamWriter.WriteLine(timeStamp + " " + eventText);
                        Console.WriteLine(timeStamp + " " + eventText);
                    }
                    else // Print a blank line.
                    {
                        streamWriter.WriteLine("");
                        Console.WriteLine("");
                    }

                    streamWriter.Flush();
                    streamWriter.Close();
                    return;
                }
                catch (Exception e)
                {
                    WriteEvent(timeStamp + " " + e.Message + " [Log.WriteFile]", Log.Error);

                    try
                    {
                        Console.WriteLine(timeStamp + " " + e.Message + " [Log.WriteFile]");
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Writes event text to the event viewer application log for entry type.
        /// </summary>
        private static void WriteEvent(string eventText, string entryType)
        {
            try
            {
                /*EventLog eventLog = new EventLog();

                eventLog.Log = "Application";
                eventLog.Source = name;

                eventLog.WriteEntry(eventText, entryType);
                eventLog.Close();*/
            }
            catch (Exception e)
            {
                WriteFile(e.Message + " [Log.WriteEvent]");
            }
        }
    }
}
