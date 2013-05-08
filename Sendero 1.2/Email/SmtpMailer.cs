using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Anetics.Common;

namespace Anetics.Email
{
  public class SmtpMailer
  {
    private const int COMMAND_LATENCY = 50;

    private string host;
    private string userId;
    private string password;
    
    private int port = 25;
    private int timeout = 55;

    private IPAddress hostIp;

    private TcpClient tcpClient = null;
    private NetworkStream networkStream = null;
    private StreamReader streamReader = null;

    public SmtpMailer(string host, string userId, string password)
    {
      this.host = host;
      this.userId = userId;
      this.password = password;

      HostIpSet(host);
    }

    public int Port
    {
      set
      {
        port = value;
      }
    }

    public int Timeout
    {
      set
      {
        timeout = value;
      }
    }

    public void MessageSend(string to, string from, string subject, string content)
    {
      if (Login())
      {
        try
        {
          string command = "MAIL FROM: " + "<" + from + ">\r\n";
          byte[] commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

          networkStream.Write(commandBytes, 0, commandBytes.Length);
          Log.Write("Command: " + command + " [SmtpMailer.MessageSend]", 4);
          Thread.Sleep(COMMAND_LATENCY);
          
          string response = streamReader.ReadLine(); 
          Log.Write("Response: " + response + " [SmtpMailer.MessageSend]", 4);

          if (int.Parse(response.Substring(0, 3)) > 299)
          {
            throw new Exception("Error: " + response + " [SmtpMailer.MessageSend]");
          }

          string delimiter = ";, ";
          char[] delimiters = delimiter.ToCharArray();
          string[] recipient = to.Split(delimiters);

          for (int i = 0; i < recipient.Length; ++i)
          {
            command = "RCPT TO: " + "<" + recipient[i] + ">\r\n";
            commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

            networkStream.Write(commandBytes, 0, commandBytes.Length);
            Log.Write("Command: " + command + " [SmtpMailer.MessageSend]", 4);
            Thread.Sleep(COMMAND_LATENCY);
          
            response = streamReader.ReadLine(); 
            Log.Write("Response: " + response + " [SmtpMailer.MessageSend]", 4);

            if (int.Parse(response.Substring(0, 3)) > 299)
            {
              throw new Exception("Error: " + response + " [SmtpMailer.MessageSend]");
            }
          }

          command = "DATA\r\n";
          commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

          networkStream.Write(commandBytes, 0, commandBytes.Length);
          Log.Write("Command: " + command + " [SmtpMailer.MessageSend]", 4);
          Thread.Sleep(COMMAND_LATENCY);
          
          response = streamReader.ReadLine(); 
          Log.Write("Response: " + response + " [SmtpMailer.MessageSend]", 4);

          if (int.Parse(response.Substring(0, 3)) < 400)
          {
            command = "Subject: " + subject + (subject.EndsWith("\r\n") ? "" : "\r\n");
            command += "From: " + from + (from.EndsWith("\r\n") ? "\r\n" : "\r\n\r\n");
            command += content + (content.EndsWith("\r\n") ? "" : "\r\n");
            command += ".\r\n";
            commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

            networkStream.Write(commandBytes, 0, commandBytes.Length);
            Log.Write("Command: " + command + " [SmtpMailer.MessageSend]", 4);
            Thread.Sleep(COMMAND_LATENCY);
          
            response = streamReader.ReadLine(); 
            Log.Write("Response: " + response + " [SmtpMailer.MessageSend]", 4);

            if (int.Parse(response.Substring(0, 3)) > 299)
            {
              throw new Exception("Error: " + response + " [SmtpMailer.MessageSend]");
            }
          }
        }
        catch
        {
          throw;
        }
        finally
        {
          Logout();
        }
      }
      else
      {
        throw new Exception("Login failed. [SmtpMailer.MessageSend]");
      }
    }

    private bool Login()
    {
      string response;
      string localHost = "Unknown";

      try
      {
        localHost = Environment.MachineName;
      }
      catch {}

      string command = "EHLO " + localHost + "\r\n";
      byte[] commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

      try
      {
        tcpClient = new TcpClient();
        tcpClient.ReceiveTimeout = timeout;

        tcpClient.Connect(hostIp, port);
        Log.Write("Login attempt to " + host + " [" + hostIp.ToString() + "], port " + port + ". [SmtpMailer.Login]", 3);

        networkStream = tcpClient.GetStream();
        streamReader = new StreamReader(tcpClient.GetStream());

        response = streamReader.ReadLine();
        Log.Write("Response: " + response + " [SmtpMailer.Login]", 4);
        
        networkStream.Write(commandBytes, 0, commandBytes.Length);
        Log.Write("Command: " + command + " [SmtpMailer.Login]", 4);
        Thread.Sleep(COMMAND_LATENCY);

        response = streamReader.ReadLine();
        Log.Write("Response: " + response + " [SmtpMailer.Login]", 4);
        
        if (response.StartsWith("250"))
        {
          string s = streamReader.ReadLine();
          while (s[3].Equals('-'))
          {
            response += "\n" + s;
            s = streamReader.ReadLine();
          }
          Log.Write("Response: " + response + " [SmtpMailer.Login]", 4);
        }
        else
        {
          return false;
        }

        if (response.ToUpper().IndexOf("AUTH LOGIN") > -1)
        {
          command = "AUTH LOGIN\r\n";
          commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());
          
          networkStream.Write(commandBytes, 0, commandBytes.Length);
          Log.Write("Command: " + command + " [SmtpMailer.Login]", 4);
          Thread.Sleep(COMMAND_LATENCY);
          
          response = streamReader.ReadLine();
          Log.Write("Response: " + response + " [SmtpMailer.Login]", 4);
          
          commandBytes = Encoding.ASCII.GetBytes(userId.ToCharArray());
          command = System.Convert.ToBase64String(commandBytes) + "\r\n";
          commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());
          
          networkStream.Write(commandBytes, 0, commandBytes.Length);
          Log.Write("Command: " + command + " [SmtpMailer.Login]", 4);
          Thread.Sleep(COMMAND_LATENCY);
          
          response = streamReader.ReadLine();
          Log.Write("Response: " + response + " [SmtpMailer.Login]", 4);
          
          commandBytes = Encoding.ASCII.GetBytes(password.ToCharArray());
          command = System.Convert.ToBase64String(commandBytes) + "\r\n";
          commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());
          
          networkStream.Write(commandBytes, 0, commandBytes.Length);
          Log.Write("Command: " + command + " [SmtpMailer.Login]", 4);
          Thread.Sleep(COMMAND_LATENCY);
          
          response = streamReader.ReadLine();
          Log.Write("Response: " + response + " [SmtpMailer.Login]", 4);
          
          if (int.Parse(response.Substring(0, 3)) > 299)
          {
            return false;
          }

          return true;
        }
        else
        {
          return true;
        }
      }
      catch
      {
        return false;
      }
    }

    private void Logout()
    {
      string response;

      string command = "QUIT\r\n";
      byte[] commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

      if (networkStream != null)
      {
        try
        {
          networkStream.Write(commandBytes, 0, commandBytes.Length);
          Log.Write("Command: " + command + " [SmtpMailer.Logout]", 4);
          Thread.Sleep(COMMAND_LATENCY);

          response = streamReader.ReadLine();
          Log.Write("Response: " + response + " [SmtpMailer.Logout]", 4);
          Thread.Sleep(COMMAND_LATENCY * 10);
        }
        catch
        {
          throw;
        }
        finally
        {
          networkStream.Close();
        }
      }
    }

    private void HostIpSet(string host)
    {
      Match match = Regex.Match(host, "[0-9]+\\.[0-9]+\\.[0-9]+\\.[0-9]+");

      if (match.Success)
      {
        hostIp = IPAddress.Parse(host);
      }
      else
      {
        IPHostEntry iPHostEntry = Dns.GetHostByName(host);
        
        if (iPHostEntry.AddressList.Length.Equals(0)) 
        {
          throw new Exception("Unable to resolve IP address for " + host + ".");
        }

        hostIp = iPHostEntry.AddressList[0];
      }
    }
	}
}
