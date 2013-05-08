using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Anetics.Common;

namespace Anetics.Courier
{
	class PopFetch
	{
    private const int COMMAND_LATENCY = 50;

    private string host;
    private string userId;
    private string password;
    
    private int port = 110;
    private int timeout = 55;

    private TcpClient tcpClient = null;
    private NetworkStream networkStream = null;
    private StreamReader streamReader = null;

    private int messageCount = 0;

    public PopFetch(string host, string userId, string password)
    {
      this.host = host;
      this.userId = userId;
      this.password = password;
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

    public EmailHeader[] HeadersGet()
    {
      if (Login())
      {
        EmailHeader[] emailHeader = new EmailHeader[messageCount];

        try
        {
          for (int n = 0; n < messageCount; n++)
          {
            string command = "TOP " + (n + 1) + " 0" + "\r\n";
            byte[] commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

            networkStream.Write(commandBytes, 0, commandBytes.Length);
            Log.Write("Command: " + command + " [PopFetch.HeadersGet]", 4);
            Thread.Sleep(COMMAND_LATENCY);

            string response = streamReader.ReadLine(); 
            Log.Write("Response: " + response + " [PopFetch.HeadersGet]", 4);

            if (response.StartsWith("+OK"))
            {
              response = "";
              string s = streamReader.ReadLine();

              while (!s.Equals("."))
              {
                response += s + "\n";
                s = streamReader.ReadLine();
              }
              Log.Write("Response: " + response + " [PopFetch.HeadersGet]", 4);

              emailHeader[n].From = HeaderValue("From", ref response);
              emailHeader[n].Subject = HeaderValue("Subject", ref response);
              emailHeader[n].Date = HeaderValue("Date", ref response);
              emailHeader[n].OkToDelete = true;
            }
            else
            {
              throw new Exception("Error [" + response + "] getting header for index " + (n + 1) + " [PopFetch.HeadersGet]");  
            }
          }

          Log.Write("Returned " + emailHeader.Length + " header items. [PopFetch.HeadersGet]", 3);
          return emailHeader;
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
        throw new Exception("Login failed. [PopFetch.HeadersGet]");
      }
    }

    public string MessageGet(int index)
    {
      if (Login())
      {
        try 
        {
          string command = "RETR " + index.ToString() + "\r\n";
          byte[] commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

          networkStream.Write(commandBytes, 0, commandBytes.Length);
          Log.Write("Command: " + command + " [PopFetch.MessageGet]", 4);
          Thread.Sleep(COMMAND_LATENCY);
          
          string response = streamReader.ReadLine(); 
          Log.Write("Response: " + response + " [PopFetch.MessageGet]", 4);

          if (response.StartsWith("+OK"))
          {
            response = "";
            string s = streamReader.ReadLine();

            while (!s.Equals("."))
            {
              response += s + '\n';
              s = streamReader.ReadLine();
            }
            Log.Write("Content: " + response + " [PopFetch.MessageGet]", 4);
          }
          else
          {
            throw new Exception("Error [" + response + "] getting message for index " + index + " [PopFetch.MessageGet]");  
          }

          Log.Write("Returned message for index " + index + ". [PopFetch.MessageGet]", 3);
          return response;
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
        throw new Exception("Login failed. [PopFetch.MessageGet]");
      }
    }

    public void MessagePurge(int purgeCount)
    {
      if (Login())
      {
        try
        {
          for (int n = messageCount; n > messageCount - purgeCount; n--)
          {
            string command = "DELE " + n + "\r\n";
            byte[] commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

            networkStream.Write(commandBytes, 0, commandBytes.Length);
            Log.Write("Command: " + command + " [PopFetch.MessagesDelete]", 4);
            Thread.Sleep(COMMAND_LATENCY);

            string response = streamReader.ReadLine(); 
            Log.Write("Response: " + response + " [PopFetch.MessagesDelete]", 4);

            if (!response.StartsWith("+OK"))
            {
              throw new Exception("Error [" + response + "] deleting message for index " + n + " [PopFetch.MessagesDelete]");  
            }
          }

          Log.Write("Deleted " + purgeCount + " messages. [PopFetch.MessagesDelete]", 3);
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
        throw new Exception("Login failed. [PopFetch.HeadersGet]");
      }
    }

    public void MessageDelete(int messageIndex, int messageCount)
    {
      if (Login())
      {
        try
        {
          string command = "DELE " + (messageIndex + messageCount - this.messageCount) + "\r\n";
          byte[] commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

          networkStream.Write(commandBytes, 0, commandBytes.Length);
          Log.Write("Command: " + command + " [PopFetch.MessagesDelete]", 4);
          Thread.Sleep(COMMAND_LATENCY);

          string response = streamReader.ReadLine(); 
          Log.Write("Response: " + response + " [PopFetch.MessagesDelete]", 4);

          if (!response.StartsWith("+OK"))
          {
            throw new Exception("Error [" + response + "] deleting message for index " + (messageIndex + messageCount - this.messageCount) + " [PopFetch.MessagesDelete]");  
          }

          Log.Write("Deleted " + messageCount + " messages. [PopFetch.MessagesDelete]", 3);
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
        throw new Exception("Login failed. [PopFetch.HeadersGet]");
      }
    }

    private bool Login()
    {
      string response;

      string command = "USER " + userId + "\r\n";
      byte[] commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

      try
      {
        tcpClient = new TcpClient();
        tcpClient.ReceiveTimeout = timeout;

        tcpClient.Connect(host, port);
        Log.Write("Login attempt to " + host + ", port " + port + ". [PopFetch.Login]", 3);

        networkStream = tcpClient.GetStream();
        streamReader = new StreamReader(tcpClient.GetStream());
        
        response = streamReader.ReadLine();
        Log.Write("Response: " + response + " [PopFetch.Login]", 4);
        
        networkStream.Write(commandBytes, 0, commandBytes.Length);
        Log.Write("Command: " + command + " [PopFetch.Login]", 4);
        Thread.Sleep(COMMAND_LATENCY);

        response = streamReader.ReadLine();
        Log.Write("Response: " + response + " [PopFetch.Login]", 4);

        if (response.StartsWith("+OK"))
        {
          command = "PASS " + password + "\r\n";
          commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

          networkStream.Write(commandBytes, 0, commandBytes.Length);
          Log.Write("Command: " + command + " [PopFetch.Login]", 4);
          Thread.Sleep(COMMAND_LATENCY);

          response = streamReader.ReadLine();
          Log.Write("Response: " + response + " [PopFetch.Login]", 4);

          if (response.StartsWith("+OK"))
          {
            command = "STAT \r\n";
            commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

            networkStream.Write(commandBytes, 0, commandBytes.Length);
            Log.Write("Command: " + command + " [PopFetch.Login]", 4);
            Thread.Sleep(COMMAND_LATENCY);

            response = streamReader.ReadLine();
            Log.Write("Response: " + response + " [PopFetch.Login]", 4);

            if (response.StartsWith("+OK"))
            {
              messageCount = int.Parse(Tools.SplitItem(response, " " , 1));
              Log.Write("MessageCount: " + messageCount + " [PopFetch.Login]", 3);

              return true;
            }
          
            return false;
          }

          return false;
        }

        return false;
      }
      catch
      {
        Logout();
        throw;
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
          Log.Write("Command: " + command + " [PopFetch.Logout]", 4);
          Thread.Sleep(COMMAND_LATENCY);

          response = streamReader.ReadLine();
          Log.Write("Response: " + response + " [PopFetch.Logout]", 4);
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

    private string HeaderValue(string headerName, ref string headers)
    {
      int startIndex;

      headerName = ("\n" + headerName + ": ").ToLower();
      
      if ((startIndex = headers.ToLower().IndexOf(headerName)) > -1)
      {
        startIndex += headerName.Length;
        return headers.Substring(startIndex, headers.IndexOf('\n', startIndex) - startIndex);
      }

      return "";
    }
	}
}