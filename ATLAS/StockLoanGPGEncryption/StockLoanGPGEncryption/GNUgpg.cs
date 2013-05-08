using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace StockLoan.Transport
{
    public enum Commands
    {
        Encrypt,
        EncryptSign,
        Decrypt,
        Verify
    }

    public enum VerbosityLevels
    {
        None,
        Verbose,
        VeryVerbose
    }

    public class GnuPG
    {
        private Commands command = Commands.Decrypt;

        private string recipient = "";
        private string passPhrase = "";
        private string originator = "";
        private int timeout = 60000;
        private int exitCode = 0;

        private VerbosityLevels verbosity = VerbosityLevels.None;

        private string file;
        private string homePath;
        private string loadExtension = "";

        private bool noMangleFileNames = false;

        private Process process;
        private string processInfo;


        public GnuPG(string homePath) : this(homePath, "") { }
        public GnuPG(string homePath, string loadExtension)
        {
            this.homePath = homePath;
            this.loadExtension = loadExtension;

            Console.WriteLine("GnuPG loaded with a home path of '" + homePath + "' and an extension of '" + loadExtension + "'. [GnuPG.GnuPG]");
        }

        public void DoCommand(string file)
        {
            this.file = file;

            Console.WriteLine("Call for " + command.ToString() + " on " + file + ". [GnuPG.DoCommand]");

            ProcessStartInfo processStartInfo = new ProcessStartInfo(homePath + "gpg.exe", CommandArgs());

            processStartInfo.WorkingDirectory = homePath;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;

            processStartInfo.RedirectStandardInput = true; // To send pass phrase.
            processStartInfo.RedirectStandardError = true; // To collect process info.

            process = Process.Start(processStartInfo);

            if (!passPhrase.Equals(""))
            {
                process.StandardInput.WriteLine(passPhrase);
                process.StandardInput.Flush();
            }

            processInfo = "GnuPG ";

            ThreadStart errorReader = new ThreadStart(StandardErrorReader); // Separate thread to not deadlock.
            Thread errorThread = new Thread(errorReader);
            errorThread.Start();

            if (process.WaitForExit(timeout)) // Process finished before timeout.
            {
                if (!errorThread.Join(timeout / 2)) // Wait for thread but timeout if no return.
                {
                    errorThread.Abort();
                    throw new Exception("GnuPG error thread join timed out. [GnuPG.DoCommand]");
                }
            }
            else // Process suspended, kill all.
            {
                process.Kill();

                if (errorThread.IsAlive)
                {
                    errorThread.Abort();
                }

                throw new Exception("GnuPG process timed out. [GnuPG.DoCommand]");
            }

            exitCode = process.ExitCode;

            if (exitCode > 2) // More than just a warning.
            {
                if (processInfo.Equals("GnuPG "))
                {
                    processInfo = "GnuPG exit code " + exitCode + ", unknown error. [GnuPG.DoCommand]";
                }

                throw new Exception(processInfo);
            }
        }

        protected string CommandArgs()
        {
            StringBuilder commandArgs = new StringBuilder("", 500);

            if (noMangleFileNames)
            {
                commandArgs.Append("--no-mangle-dos-filenames ");
            }

            if (!loadExtension.Equals(""))
            {
                commandArgs.Append("--load-extension ");
                commandArgs.Append(loadExtension);
                commandArgs.Append(" ");
            }

            switch (verbosity)
            {
                case VerbosityLevels.None:
                    commandArgs.Append("--no-verbose ");
                    break;
                case VerbosityLevels.Verbose:
                    commandArgs.Append("--verbose ");
                    break;
                case VerbosityLevels.VeryVerbose:
                    commandArgs.Append("--verbose --verbose ");
                    break;
            }

           /* if (!homePath.Equals(""))
            {
                commandArgs.Append("--homedir ");
                commandArgs.Append(homePath);
                commandArgs.Append(" ");
            }*/

            /*commandArgs.Append("--local-user ");
            commandArgs.Append(originator);
            commandArgs.Append(" ");*/

            switch (command)
            {
                case Commands.Encrypt:
                    commandArgs.Append("--recipient ");
                    commandArgs.Append(recipient);
                    commandArgs.Append(" -e ");
                    commandArgs.Append(file);
                    break;
                case Commands.EncryptSign:
                    commandArgs.Append("--sign --recipient ");
                    commandArgs.Append(recipient);
                    commandArgs.Append(" --passphrase-fd 0 --yes --encrypt-files ");
                    commandArgs.Append(file);
                    break;
                case Commands.Decrypt:
                    commandArgs.Append("--passphrase-fd 0 --yes --decrypt-files ");
                    commandArgs.Append(file);
                    break;
            }

            Console.WriteLine("CommandArgs returning: " + commandArgs.ToString() + " [GnuPG.CommandArgs]");
            return commandArgs.ToString();
        }

        public void StandardErrorReader()
        {
            string standardError = process.StandardError.ReadToEnd();

            lock (this)
            {
                processInfo += standardError;
            }
        }

        public string ProcessInfo
        {
            get
            {
                return processInfo;
            }
        }

        public int ExitCode
        {
            get
            {
                return exitCode;
            }
        }

        public int Timeout
        {
            set
            {
                timeout = value;
            }

            get
            {
                return timeout;
            }
        }

        public VerbosityLevels Verbosity
        {
            set
            {
                verbosity = value;
            }

            get
            {
                return verbosity;
            }
        }

        public string Originator
        {
            set
            {
                originator = value;
            }
        }

        public string PassPhrase
        {
            set
            {
                passPhrase = value;
            }
        }

        public string Recipient
        {
            set
            {
                recipient = value;
            }
        }

        public Commands Command
        {
            set
            {
                command = value;
            }
        }

        public bool NoMangleFileNames
        {
            set
            {
                noMangleFileNames = value;
            }

            get
            {
                return noMangleFileNames;
            }
        }
    }
}

