using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Web;

public class LogWriter
{
    private static string logFile;

    public LogWriter()
    {
        logFile = ConfigurationManager.AppSettings["LogFile"];
    }

    public void Write(string emailAddress)
    {
        try
        {
            TextWriter txtWrite = null;

            if (File.Exists(logFile))
            {
                txtWrite = new StreamWriter(File.Open(logFile, FileMode.Append, FileAccess.Write));
            }
            else
            {
                txtWrite = new StreamWriter(File.Open(logFile, FileMode.OpenOrCreate, FileAccess.Write));
            }           
            
            txtWrite.WriteLine(emailAddress);
            txtWrite.Close();
        }
        catch { }
    }
}