using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StockLoan.FileTransfer
{
    public class VendorEncryption
    {


        public static void GnuGPGImportKey(
            string executablePath,
            string publicKeyFilePath)
        {
            Process process;
            string passPhrase = "";
            string processInfo = "";
            string arguments = "";
            int exitCode = 0;
            int timeout = 6000;

            if (!publicKeyFilePath.Equals(""))
            {
                arguments = "--import " + publicKeyFilePath;
            }


            Console.WriteLine("Importing key for " + publicKeyFilePath);
            ProcessStartInfo processStartInfo = new ProcessStartInfo(executablePath, arguments);
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;

            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardError = true;

            process = Process.Start(processStartInfo);


            if (!passPhrase.Equals(""))
            {
                process.StandardInput.WriteLine(passPhrase);
                process.StandardInput.Flush();
            }

            processInfo = "GnuPG ";


            if (!process.WaitForExit(timeout))
            {
                process.Kill();


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
        
        
        public static void GnuGPGEncrypt(
            string executablePath,           
            string originalFilePath)
        {
            Process process;
            string passPhrase = "";
            string processInfo = "";
            string arguments = "";
            int exitCode = 0;
            int timeout = 6000;

            arguments = "--recipient stockloan@pirum.com --encrpypt " + originalFilePath;


            ProcessStartInfo processStartInfo = new ProcessStartInfo(executablePath, arguments);       
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;

            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardError = true;

            process = Process.Start(processStartInfo);


            if (!passPhrase.Equals(""))
            {
                process.StandardInput.WriteLine(passPhrase);
                process.StandardInput.Flush();
            }

            processInfo = "GnuPG ";
     
            if (!process.WaitForExit(timeout))
            {
                process.Kill();

               
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
    }
}
