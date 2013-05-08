using System;
using IBM.WMQ;
using System.Threading;
using Anetics.Common;

namespace Anetics.Loanet
{
	public class MqSeries
	{
    private int delayOnQueueErrorSeconds;
    private int delayOnBizDateRollSeconds;
    private int delayOnActivitySuspendSeconds;

    private Thread waitReplyThread = null;
    private Thread waitActivityThread = null;

    private bool waitReplyIsStopped = true;
    private bool waitActivityIsStopped = true;

    private int waitReplySeconds;
    private int waitActivitySeconds;

    private string replyQueueManagerName;
    private string activityQueueManagerName;
    
    private string replyQueueName;
    private string activityQueueName;
    
    private ReplyHandlerDelegate replyHandler;
    private ActivityHandlerDelegate activityHandler;

    private System.Text.ASCIIEncoding ascii;

    public MqSeries(string hostName, string channel, int port)
		{      
      ascii = new System.Text.ASCIIEncoding();
          
      try 
      {
        MQEnvironment.Hostname = hostName;
        MQEnvironment.Channel = channel;
        MQEnvironment.Port = port;
        
        delayOnQueueErrorSeconds = int.Parse(Standard.ConfigValue("DelayOnQueueErrorSeconds", "150"));
        delayOnBizDateRollSeconds = int.Parse(Standard.ConfigValue("DelayOnBizDateRollSeconds", "1200"));
        delayOnActivitySuspendSeconds = int.Parse(Standard.ConfigValue("DelayOnActivitySuspendSeconds", "0"));
      }
      catch (Exception e) 
      {
        Log.Write(e.Message + " [MqSeries.MqSeries]", Log.Error, 1);
      }
		}

    public void WaitReplyStart(string queueManagerName, string queueName, int waitSeconds, ReplyHandlerDelegate replyHandler)
    {
      this.replyQueueManagerName = queueManagerName;
      this.replyQueueName = queueName;
      this.waitReplySeconds = waitSeconds;
      this.replyHandler = replyHandler;

      waitReplyIsStopped = false;

      if ((waitReplyThread == null)||(!waitReplyThread.IsAlive)) // Must create new thread.
      {
        waitReplyThread = new Thread(new ThreadStart(WaitReply));
        waitReplyThread.Name = "WaitReply";
        waitReplyThread.Start();

        Log.Write("Starting with new wait-reply thread. [MqSeries.WaitReplyStart]", 2);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Starting with wait-reply thread already running. [MqSeries.WaitReplyStart]", 2);
      }
    }

    public void WaitReplyStop()
    {
      waitReplyIsStopped = true;

      if (waitReplyThread == null)
      {
        Log.Write("Stopping with wait-reply thread never having started. [MqSeries.WaitReplyStop]", 2);
      }
      else if (waitReplyThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        waitReplyThread.Abort();
        Log.Write("Sleeping wait-reply thread has aborted. [MqSeries.WaitReplyStop]", 2);
      }
      else
      {
        Log.Write("Stopping with wait-reply thread still active. [MqSeries.WaitReplyStop]", 2);
      }
    }

    private void WaitReply()
    {
      MQQueueManager        queueManager = null;
      MQQueue               queue = null;
      MQMessage             message = null;
      MQGetMessageOptions   messageOptions = null;
      
      while (!waitReplyIsStopped)
      {
        try 
        {
          if (queueManager == null)
          {
            queueManager = new MQQueueManager(replyQueueManagerName);
          }

          queue = queueManager.AccessQueue(replyQueueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_BROWSE + MQC.MQOO_FAIL_IF_QUIESCING);
          message = new MQMessage();
          messageOptions = new MQGetMessageOptions();
          messageOptions.Options = MQC.MQGMO_WAIT + MQC.MQGMO_BROWSE_FIRST + MQC.MQGMO_FAIL_IF_QUIESCING;
          messageOptions.WaitInterval = waitReplySeconds * 1000;

          queue.Get(message, messageOptions); // This first message get is to peek, leaving the message on the queue if processing fails.
          byte[] messageBytes = new byte[message.TotalMessageLength];
          message.ReadFully(ref messageBytes, 0, message.TotalMessageLength);

          if (replyHandler(ascii.GetString(message.CorrelationId).Trim(), ascii.GetString(messageBytes))) // Now OK to pull message off the queue and discard.
          { 
            Log.Write("New message, handler process status OK. [MqSeries.WaitReply]", 2);

            message = new MQMessage();            
            messageOptions = new MQGetMessageOptions();
            messageOptions.Options = MQC.MQGMO_MSG_UNDER_CURSOR + MQC.MQGMO_NO_SYNCPOINT;
            
            queue.Get(message, messageOptions);
          }
          else
          {
            Log.Write("New message, handler process failed. [MqSeries.WaitReply]", Log.Error, 1);
            Thread.Sleep(delayOnQueueErrorSeconds * 1000);
          }
        }
        catch (MQException mqe) 
        {
          if (mqe.Reason != MQC.MQRC_NO_MSG_AVAILABLE)
          {
            queueManager = null;
            Log.Write(mqe.Message + " [MqSeries.WaitReply]", Log.Error, 1);
            Thread.Sleep(delayOnQueueErrorSeconds * 1000);
          }
          else
          {
            Log.Write("No new message. [MqSeries.WaitReply]", 3);
          }
        }
        catch (Exception e) 
        {
          Log.Write(e.Message + " [MqSeries.WaitReply]", Log.Error, 1);
          Thread.Sleep(delayOnQueueErrorSeconds * 1000);
        }
        finally
        {
          try
          {
            if (queue.IsOpen)
            {
              queue.Close();
            }
          }
          catch {}
        }
      }
    }

    public void WaitActivityStart(string queueManagerName, string queueName, int waitSeconds, ActivityHandlerDelegate activityHandler)
    {
      this.activityQueueManagerName = queueManagerName;
      this.activityQueueName = queueName;
      this.waitActivitySeconds = waitSeconds;
      this.activityHandler = activityHandler;

      waitActivityIsStopped = false;

      if ((waitActivityThread == null)||(!waitActivityThread.IsAlive)) // Must create new thread.
      {
        waitActivityThread = new Thread(new ThreadStart(WaitActivity));
        waitActivityThread.Name = "WaitActivity";
        waitActivityThread.Start();

        Log.Write("Starting with new wait-activity thread. [MqSeries.WaitReplyStart]", 2);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Starting with wait-activity thread already running. [MqSeries.WaitActivityStart]", 2);
      }
    }

    public void WaitActivityStop()
    {
      waitActivityIsStopped = true;

      if (waitActivityThread == null)
      {
        Log.Write("Stopping with wait-activity thread never having started. [MqSeries.WaitActivityStop]", 2);
      }
      else if (waitActivityThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        waitActivityThread.Abort();
        Log.Write("Sleeping wait-activity thread has aborted. [MqSeries.WaitActivityStop]", 2);
      }
      else
      {
        Log.Write("Stopping with wait-activity thread still active. [MqSeries.WaitActivityStop]", 2);
      }
    }

    private void WaitActivity()
    {
      MQQueueManager        queueManager = null;
      MQQueue               queue = null;
      MQMessage             message = null;
      MQGetMessageOptions   messageOptions = null;

      while (!waitActivityIsStopped)
      {
        try 
        {
          if (queueManager == null)
          {
            queueManager = new MQQueueManager(activityQueueManagerName);
          }

          queue = queueManager.AccessQueue(activityQueueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_BROWSE + MQC.MQOO_FAIL_IF_QUIESCING);

          message = new MQMessage();
          messageOptions = new MQGetMessageOptions();
          messageOptions.Options = MQC.MQGMO_WAIT + MQC.MQGMO_BROWSE_FIRST + MQC.MQGMO_FAIL_IF_QUIESCING; 
          messageOptions.WaitInterval = waitActivitySeconds * 1000;

          queue.Get(message, messageOptions); // This first message get is to peek, leaving the message on the queue if processing fails.
          byte[] messageBytes = new byte[message.TotalMessageLength];
          message.ReadFully(ref messageBytes, 0, message.TotalMessageLength);

          if (message.PutDateTime.ToString(Standard.DateFormat).CompareTo(Master.ContractsBizDate) > 0)
          {
            Log.Write("Waiting for contracts to role to " + message.PutDateTime.ToString(Standard.DateFormat) + ". [MqSeries.WaitActivity]", 2);
            Thread.Sleep(delayOnBizDateRollSeconds * 1000);
            continue;
          }
          else if (message.PutDateTime.ToString(Standard.DateFormat).CompareTo(Master.ContractsBizDate) < 0)
          {
            Log.Write("Discarding a message from " + message.PutDateTime.ToString(Standard.DateTimeFileFormat) + ". [MqSeries.WaitActivity]", 2);
            Log.Write(ascii.GetString(messageBytes) + ". [MqSeries.WaitActivity]", 3);

            message = new MQMessage();
            messageOptions = new MQGetMessageOptions();
            messageOptions.Options = MQC.MQGMO_MSG_UNDER_CURSOR;
            
            queue.Get(message, messageOptions);
            continue;
          }
          else if (delayOnActivitySuspendSeconds > 0)
          {
            Log.Write("Suspending WaitActivity for " + delayOnActivitySuspendSeconds.ToString("#,##0") + " seconds. [MqSeries.WaitActivity]", 2);
            Thread.Sleep(delayOnActivitySuspendSeconds * 1000);
            continue;
          }

          if (activityHandler(ascii.GetString(messageBytes))) // Now OK to pull message off the queue and discard.
          { 
            Log.Write("New message, handler process status OK. [MqSeries.WaitActivity]", 2);

            message = new MQMessage();
            messageOptions = new MQGetMessageOptions();
            messageOptions.Options = MQC.MQGMO_MSG_UNDER_CURSOR  + MQC.MQGMO_NO_SYNCPOINT;
            
            queue.Get(message, messageOptions);
          }
          else
          {
            Log.Write("New message, handler process failed. [MqSeries.WaitActivity]", Log.Error, 1);
            Thread.Sleep(delayOnQueueErrorSeconds * 1000);
          }
        }
        catch (MQException mqe) 
        {
          if (mqe.Reason != MQC.MQRC_NO_MSG_AVAILABLE)
          {
            queueManager = null;
            Log.Write(mqe.Message + " [MqSeries.WaitActivity]", Log.Error, 1);
            Thread.Sleep(delayOnQueueErrorSeconds * 1000);
          }
          else
          {
            Log.Write("No new message. [MqSeries.WaitActivity]", 3);
          }
        }
        catch (Exception e) 
        {
          Log.Write(e.Message + " [MqSeries.WaitActivity]", Log.Error, 1);
          Thread.Sleep(delayOnQueueErrorSeconds * 1000);
        }
        finally
        {
          try
          {
            if (queue.IsOpen)
            {
              queue.Close();
            }
          }
          catch {}
        }
      }
    }

    public void Put(
      string queueManagerName,
      string queueName,
      string credentials,
      string messageId,
      string messageText,
      string replyToQueueManager,
      string replyToQueue)
    {
      Put(
        queueManagerName,
        queueName,
        credentials,
        messageId,
        messageText,
        Standard.ConfigValue("RemoteQueueManagerName"),
        replyToQueueManager,
        replyToQueue);
    }
      
    public void Put(
      string queueManagerName,
      string queueName,
      string credentials,
      string messageId,
      string messageText,
      string remoteQueueManager,
      string replyToQueueManager,
      string replyToQueue)
    {
      MQQueueManager        queueManager = null;
      MQQueue               queue = null;
      MQMessage             message = null;
      MQPutMessageOptions   messageOptions = null;

      Log.Write("QueueManagerName: " + queueManagerName + " [MqSeries.Put]", 3);
      Log.Write("QueueName: " + queueName + " [MqSeries.Put]", 3);
      Log.Write("Credentials: " + credentials + " [MqSeries.Put]", 3);
      Log.Write("MessageId: " + messageId + " [MqSeries.Put]", 3);
      Log.Write("RemoteQueueManager: " + Tools.ZeroLengthNull(remoteQueueManager) + " [MqSeries.Put]", 3);
      Log.Write("ReplyToQueueManager: " + replyToQueueManager + " [MqSeries.Put]", 3);
      Log.Write("ReplyToQueue: " + replyToQueue + " [MqSeries.Put]", 3);
      Log.Write("MessageText:\n" + messageText + "\n [MqSeries.Put]", 3);
    
      try 
      {
        if (queueManager == null)
        {
          queueManager = new MQQueueManager(queueManagerName);
        }

        if ((remoteQueueManager == null) || (remoteQueueManager.Equals("")))
        {        
          queue = queueManager.AccessQueue(queueName,
            MQC.MQOO_OUTPUT +
            MQC.MQOO_FAIL_IF_QUIESCING + MQC.MQOO_SET_IDENTITY_CONTEXT);
        }
        else
        {
          queue = queueManager.AccessQueue(queueName,
            MQC.MQOO_OUTPUT +
            MQC.MQOO_FAIL_IF_QUIESCING + MQC.MQOO_SET_IDENTITY_CONTEXT ,
            remoteQueueManager,
            null,
            null);
        }

        message = new MQMessage();
        message.MessageType = MQC.MQMT_REQUEST;
        message.CharacterSet = 437;
        message.ApplicationIdData = credentials.PadRight(32, ' ');        

        message.ReplyToQueueManagerName = replyToQueueManager;
        message.ReplyToQueueName = replyToQueue;
        
        message.WriteString(messageText);
        message.Format = MQC.MQFMT_STRING;

        char[] c = messageId.ToCharArray();        
        byte[] messageIdByteArray = new byte[c.Length];

        for (int i = 0; i < (c.Length); i++) // Convert chars to bytes.
        {
          messageIdByteArray[i] = (byte) c[i];
        }

        message.MessageId = messageIdByteArray;

        messageOptions = new MQPutMessageOptions();
        messageOptions.Options = MQC.MQPMO_SET_IDENTITY_CONTEXT + MQC.MQGMO_NO_SYNCPOINT;

        queue.Put(message, messageOptions);
      }
      catch (MQException mqe) 
      {
        queueManager = null;
        Log.Write(mqe.Message + " [MqSeries.Put]", Log.Error, 1);
        throw new Exception(mqe.Message);
      }
      catch (Exception e) 
      {
        Log.Write(e.Message + " [MqSeries.Put]", Log.Error, 1);
        throw;
      }
      finally
      {
        try
        {
          if (queue.IsOpen)
          {
              queue.Close();
          }
        }
        catch {}
      }
    }
  }
}
