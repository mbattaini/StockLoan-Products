// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Threading;
using System.Text.RegularExpressions;
using Anetics.Common;
using Anetics.Email;

namespace Anetics.Courier
{
  public class Email
  {
    private PopFetch popFetch;
    private EmailHeader[] emailHeader;

    private string tempStream = CourierMain.TempPath + "e-mail.stream";

    public Email(string host, string userId, string password)
    {
      popFetch = new PopFetch(host, userId, password);
      emailHeader = popFetch.HeadersGet();
    }

    public bool ExtractData(string mailAddress, string mailSubject,
      string filePathName, string fileHost, string fileUserName, string filePassword)
    {
      Stream outputStream = null;
      TextWriter textWriter = null;
      BinaryWriter outputWriter = null;

      int messageIndex = 0;

      try 
      {
        messageIndex = MessageIndex(mailAddress, mailSubject);

        if (messageIndex > 0) // Mail is available.
        {
          string messageDocument = popFetch.MessageGet(messageIndex);
          string payload = PayloadGet(ref messageDocument);

          Log.Write("Payload:\n\n" + payload + "\n\n", 4);

          if (payload.Length > 0) // We have content to process.
          {
            if (IsBase64(ref messageDocument)) // Convert the content.
            {
              byte[] outputArray = System.Convert.FromBase64String(payload);
              
              outputStream = new FileStream(tempStream, FileMode.Create);
              outputWriter = new BinaryWriter(outputStream);
              
              outputWriter.Write(outputArray);
              
              outputWriter.Close();
              outputStream.Close();
              
              if (IsXls(ref messageDocument)) // Convert to a delimited file format.
              {
                string xlsStream = CourierMain.TempPath + "xls.stream";

                XlsToDelimited(tempStream, xlsStream, '|');
                Thread.Sleep(250); // Latency to allow time for files to close.

                File.Copy(xlsStream, tempStream, true);
              }
            }
            else // Payload is plain text.
            {
              outputStream = new FileStream(tempStream, FileMode.Create);
              textWriter = new StreamWriter(outputStream);

              textWriter.Write(payload);
              
              textWriter.Close();
              outputStream.Close();
            }

            Filer filer = new Filer();
            filer.FilePut(filePathName, fileHost, fileUserName, filePassword, tempStream);

            emailHeader[messageIndex].OkToDelete = true;
            Log.Write("Email from " + mailAddress + " had content extracted. [EmailExtractData] ", 3);
          }
          else
          {
            Log.Write("Email from " + mailAddress + " had no content. [EmailExtractData] ", 2);
          }
        }

        return true; 
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [EmailExtractData] ", Log.Error, 1);
      }
      finally
      {
        if (outputStream != null)
        {
          outputStream.Close();
        }

        if (outputWriter != null)
        {
          outputWriter.Close();
        }

        if (textWriter != null)
        {
          textWriter.Close();
        }
      }

      return false; 
    }
       
    public void Purge()
    {
      for (int i = emailHeader.Length - 1; i > 0; i--)
      {
        if (emailHeader[i].OkToDelete)
        {
          popFetch.MessageDelete(i);
          Log.Write("Deleted e-mail from " + emailHeader[i].From + " subject " + emailHeader[i].Subject + " dated " + emailHeader[i].Date + ". [EmailPurge]", 2);
        }
        else
        {
          Log.Write("Saved e-mail from " + emailHeader[i].From + " subject " + emailHeader[i].Subject + " dated " + emailHeader[i].Date + ". [EmailPurge]", 2);
        }
      }
    }
    
    private string PayloadGet(ref string messageDocument)
    {
      int payloadStart = 0;                       
      int payloadEnd = 0;

      bool filenameIsEmpty = true;

      // Find payload start by finding first attachment with non-blank filename.
      while (payloadStart > -1 && filenameIsEmpty)
      {
        payloadStart = messageDocument.ToLower().IndexOf("filename=", payloadStart + 1);
        filenameIsEmpty = (messageDocument.Substring(payloadStart + 10, 1).Equals("\"") || messageDocument.Substring(payloadStart + 10, 2).Equals("0\""));
      }
      
      Log.Write("Message:\n\n" + messageDocument + "\n\n[EmailPayloadGet]", 4);

      if (payloadStart > -1) // Payload in "named" attachment starting after next blank line.
      {
        payloadStart = messageDocument.IndexOf("\n\n", payloadStart) + 2;
        
        // Ending before a following blank line.
        payloadEnd = messageDocument.IndexOf("\n\n", payloadStart);

        // Or ending before -- if no blank line.
        if (payloadEnd.Equals(-1) || (messageDocument.IndexOf("\n--", payloadStart) < payloadEnd))
        {
          payloadEnd = messageDocument.IndexOf("\n--", payloadStart) - 2;
        }
        
        Log.Write("PayloadStart: " + payloadStart + ", PayloadEnd: " + payloadEnd + ". [EmailPayloadGet]", 4);

        if (payloadEnd.Equals(-1)) // No second blank line so end at message end.
        {                                         
          return messageDocument.Substring(payloadStart);
        }
        else
        {
          return messageDocument.Substring(payloadStart, payloadEnd - payloadStart);
        }
      }
      else // Payload must be in message body; return entire message body.
      {
        payloadStart = messageDocument.IndexOf("\n\n") + 2;

        return messageDocument.Substring(payloadStart);
      }
    }

    private int XlsToDelimited(string sourceFilename, string destinationFilename, char delimiter)
    {
      Stream outputStream = null;
      StreamWriter outputWriter = null;
      OleDbConnection xlsConnection = null;

      try 
      {
        int rowCount = 0;

        outputStream = new FileStream(destinationFilename, FileMode.Create);
        outputWriter = new StreamWriter(outputStream);

        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sourceFilename + ";Extended Properties=Excel 8.0;";
        xlsConnection = new OleDbConnection(connectionString);
        xlsConnection.Open();

        DataTable sheets = xlsConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] {null, null, null, "TABLE"});
        string sheetname = "[" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]";

        OleDbCommand xlsCommand = new OleDbCommand();
        xlsCommand.Connection = xlsConnection;
        xlsCommand.CommandText = @"select * from " + sheetname;

        OleDbDataAdapter xlsAdapter = new OleDbDataAdapter(xlsCommand);
        DataSet xlsData = new DataSet();

        xlsAdapter.Fill(xlsData);
        DataTable dataTable = xlsData.Tables[0];
        
        string output = "";
        bool firstRowHasData = false;

        foreach(DataColumn dataColumn in dataTable.Columns)
        {
          if (firstRowHasData = Tools.IsNumeric(dataColumn.ColumnName.Trim()))
          {
            break;
          }
        }

        if (firstRowHasData)
        {
          foreach(DataColumn dataColumn in dataTable.Columns)
          {
            output += dataColumn.ColumnName.Trim() + delimiter;
          }

          rowCount += 1;
          outputWriter.WriteLine(output.TrimEnd(delimiter));
        }

        foreach (DataRow dataRow in dataTable.Rows)
        {
          output = "";

          for (int i = 0; i < dataTable.Columns.Count; i++)
          {
            output += dataRow[i].ToString().Trim() + delimiter;
          }

          outputWriter.WriteLine(output.TrimEnd(delimiter));
          rowCount += 1;
        }





        return rowCount;
      }
      catch
      {
        Log.Write("Error converting xls file to delimitetd text file. [EmailXlsToDelimited]", 1);
        throw;
      }
      finally
      {
        if (xlsConnection != null)
        {
          if (xlsConnection.State == ConnectionState.Open)
          {
            xlsConnection.Close();
          }
        }

        if (outputWriter != null)
        {
          outputWriter.Close();
        }

        if (outputStream != null) 
        {
          outputStream.Close();
        }
      }
    }

    private bool IsBase64(ref string message)
    {
      return (message.ToLower().IndexOf("content-transfer-encoding: base64") > -1);
    }

    private bool IsXls(ref string message)
    {
      if (message.ToLower().IndexOf("ms-excel") > -1)
      {
        return true;
      }

      int startPosition = 0;                       
      bool filenameIsEmpty = true;

      while (startPosition > -1 && filenameIsEmpty)
      {
        startPosition = message.ToLower().IndexOf("filename=", startPosition + 1);
        filenameIsEmpty = (message.Substring(startPosition + 10, 1).Equals("\"") || message.Substring(startPosition + 10, 2).Equals("0\""));
      }
      
      return ((startPosition > -1) && (message.Substring(startPosition, 50).ToLower().IndexOf(".xls") > -1));
    }
    
    private int MessageIndex(string from, string subject)
    {
      if (emailHeader == null)
      {
        return 0;
      }

      for (int i = 1; i < emailHeader.Length; i++)
      {
        if ((emailHeader[i].From.IndexOf(from) > -1) && (emailHeader[i].Subject.IndexOf(subject) > -1))
        {
          return i;
        }
      }

      return 0;
    }
  }
}
