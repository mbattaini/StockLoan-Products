using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Stockloan.Common;

namespace Stockloan.Email
{
  public struct EmailHeader
  {
    public string From;
    public string Subject;
    public string Date;
    public bool   OkToDelete;
  }

	public class PopFetch
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
        EmailHeader[] emailHeader = new EmailHeader[messageCount + 1]; // Element index 0 always empty.

        try
        {
          for (int index = 1; index <= messageCount; index++) // Element index 0 always empty.
          {
            string command = "TOP " + index + " 0" + "\r\n";
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

              emailHeader[index].From = HeaderValue("From", ref response);
              emailHeader[index].Subject = HeaderValue("Subject", ref response);
              emailHeader[index].Date = HeaderValue("Date", ref response);
              emailHeader[index].OkToDelete = false;
            }
            else
            {
              throw new Exception("Error [" + response + "] getting header index " + index + ". [PopFetch.HeadersGet]");  
            }
          }

          Log.Write("Returned " + messageCount + " header items. [PopFetch.HeadersGet]", 3);
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
          string command = "RETR " + index + "\r\n";
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
              if (s.EndsWith("="))
              {
                response += s.TrimEnd('=').Replace("=0A", "\n");
              }
              else if (s.EndsWith("=20"))
              {
                response += s.Replace("=20", "\n");
              }
              else
              {
                response += s + '\n';
              }

              s = streamReader.ReadLine();
            }

            Log.Write("Content: " + response + " [PopFetch.MessageGet]", 4);
          }
          else
          {
            throw new Exception("Error [" + response + "] getting message index " + index + ". [PopFetch.MessageGet]");  
          }

          Log.Write("Returned message index " + index + ". [PopFetch.MessageGet]", 3);
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

    public void MessagePurge(int count)
    {
      if (Login())
      {
        try
        {
          for (int index = count; index > 0; index--)
          {
            string command = "DELE " + index + "\r\n";
            byte[] commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

            networkStream.Write(commandBytes, 0, commandBytes.Length);
            Log.Write("Command: " + command + " [PopFetch.MessagesDelete]", 4);
            Thread.Sleep(COMMAND_LATENCY);

            string response = streamReader.ReadLine(); 
            Log.Write("Response: " + response + " [PopFetch.MessagesDelete]", 4);

            if (!response.StartsWith("+OK"))
            {
              throw new Exception("Error [" + response + "] deleting message index " + index + ". [PopFetch.MessagePurge]");  
            }
          }

          Log.Write("Deleted " + count + " messages. [PopFetch.MessagePurge]", 3);
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
        throw new Exception("Login failed. [PopFetch.MessagePurge]");
      }
    }

    public void MessageDelete(int index)
    {
      if (Login())
      {
        try
        {
          string command = "DELE " + (index) + "\r\n";
          byte[] commandBytes = Encoding.ASCII.GetBytes(command.ToCharArray());

          networkStream.Write(commandBytes, 0, commandBytes.Length);
          Log.Write("Command: " + command + " [PopFetch.MessagesDelete]", 4);
          Thread.Sleep(COMMAND_LATENCY);

          string response = streamReader.ReadLine(); 
          Log.Write("Response: " + response + " [PopFetch.MessagesDelete]", 4);

          if (!response.StartsWith("+OK"))
          {
            throw new Exception("Error [" + response + "] deleting message index " + (index) + ". [PopFetch.MessageDelete]");  
          }

          Log.Write("Deleted message index " + index + " of " + this.messageCount + ". [PopFetch.MessageDelete]", 3);
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