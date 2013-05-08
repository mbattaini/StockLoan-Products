using System;
using IBM.WMQ;
using System.Threading;
using Anetics.Common;

namespace Anetics.S3
{
	public class MqSeries
	{
    private int delayOnQueueErrorSeconds;
    private int delayOnBizDateRollSeconds;
    private int delayOnActivitySuspendSeconds;

    private Thread waitTransactionResponseThread = null;
    private Thread waitSegEntriesThread = null;
		private Thread waitTrmLockupThread = null;
		private Thread waitTrmReleaseThread = null;

    private bool waitTransactionResponseIsStopped = true;
    private bool waitSegEntriesIsStopped = true;
		private bool waitTrmLockupIsStopped = true;
		private bool waitTrmReleaseIsStopped = true;

    private int waitTransactionResponseSeconds;
    private int waitSegEntriesSeconds;
		private int waitTrmLockupSeconds;
		private int waitTrmReleaseSeconds;

    private string transactionResponseQueueManagerName;
    private string segEntriesQueueManagerName;
		private string trmLockupQueueManagerName;
		private string trmReleaseQueueManagerName;    
    
		
		private string transactionResponseQueueName;
    private string segEntriesQueueName;
		private string trmLockupQueueName;
		private string trmReleaseQueueName;

    
    private TransactionRepsonseHandlerDelegate transactionRepsonseHandler;
    private SegEntriesHandlerDelegate segEntriesHandler;
		private TrmLockupHandlerDelegate trmLockupHandler;
		private TrmReleaseHandlerDelegate trmReleaseHandler;

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

    public void WaitTransactionResponseStart(string queueManagerName, string queueName, int waitSeconds, TransactionRepsonseHandlerDelegate transactionResponseHandler)
    {
      this.transactionResponseQueueManagerName = queueManagerName;
      this.transactionResponseQueueName = queueName;
      this.waitTransactionResponseSeconds = waitSeconds;
      this.transactionRepsonseHandler = transactionResponseHandler;

      waitTransactionResponseIsStopped = false;

      if ((waitTransactionResponseThread == null)||(!waitTransactionResponseThread.IsAlive)) // Must create new thread.
      {
        waitTransactionResponseThread = new Thread(new ThreadStart(WaitTransactionResponse));
        waitTransactionResponseThread.Name = "WaitTransactionResponse";
        waitTransactionResponseThread.Start();

        Log.Write("Starting with new wait-TransactionResponse thread. [MqSeries.WaitTransactionResponseStart]", 2);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Starting with wait-TransactionResponse thread already running. [MqSeries.WaitTransactionResponseStart]", 2);
      }
    }

    public void WaitTransactionResponseStop()
    {
      waitTransactionResponseIsStopped = true;

      if (waitTransactionResponseThread == null)
      {
        Log.Write("Stopping with wait-TransactionResponse thread never having started. [MqSeries.WaitTransactionResponseStop]", 2);
      }
      else if (waitTransactionResponseThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        waitTransactionResponseThread.Abort();
        Log.Write("Sleeping wait-TransactionResponse thread has aborted. [MqSeries.WaitTransactionResponseStop]", 2);
      }
      else
      {
        Log.Write("Stopping with wait-TransactionResponse thread still active. [MqSeries.WaitTransactionResponseStop]", 2);
      }
    }

    private void WaitTransactionResponse()
    {
      MQQueueManager        queueManager = null;
      MQQueue               queue = null;
      MQMessage             message = null;
      MQGetMessageOptions   messageOptions = null;
      
      while (!waitTransactionResponseIsStopped)
      {
        try 
        {
          if (queueManager == null)
          {
            queueManager = new MQQueueManager(transactionResponseQueueManagerName);
          }

          queue = queueManager.AccessQueue(transactionResponseQueueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_BROWSE + MQC.MQOO_FAIL_IF_QUIESCING);
          message = new MQMessage();
          messageOptions = new MQGetMessageOptions();
          messageOptions.Options = MQC.MQGMO_WAIT + MQC.MQGMO_BROWSE_FIRST + MQC.MQGMO_FAIL_IF_QUIESCING;
          messageOptions.WaitInterval = waitTransactionResponseSeconds * 1000;

          queue.Get(message, messageOptions); // This first message get is to peek, leaving the message on the queue if processing fails.
          byte[] messageBytes = new byte[message.TotalMessageLength];
          message.ReadFully(ref messageBytes, 0, message.TotalMessageLength);

          if (transactionRepsonseHandler(ascii.GetString(message.CorrelationId).Trim(), ascii.GetString(messageBytes))) // Now OK to pull message off the queue and discard.
          { 
            Log.Write("New message, handler process status OK. [MqSeries.WaitTransactionResponse]", 2);

            message = new MQMessage();            
            messageOptions = new MQGetMessageOptions();
            messageOptions.Options = MQC.MQGMO_MSG_UNDER_CURSOR;
            
            queue.Get(message, messageOptions);
          }
          else
          {
            Log.Write("New message, handler process failed. [MqSeries.WaitTransactionResponse]", Log.Error, 1);
            Thread.Sleep(delayOnQueueErrorSeconds * 1000);
          }
        }
        catch (MQException mqe) 
        {
          if (mqe.Reason != MQC.MQRC_NO_MSG_AVAILABLE)
          {
            queueManager = null;
            Log.Write(mqe.Message + " [MqSeries.WaitTransactionResponse]", Log.Error, 1);
            Thread.Sleep(delayOnQueueErrorSeconds * 1000);
          }
          else
          {
            Log.Write("No new message. [MqSeries.WaitTransactionResponse]", 3);
          }
        }
        catch (Exception e) 
        {
          Log.Write(e.Message + " [MqSeries.WaitTransactionResponse]", Log.Error, 1);
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

		public void WaitSegEntriesStart(string queueManagerName, string queueName, int waitSeconds, SegEntriesHandlerDelegate segEntriesHandler)
		{
			this.segEntriesQueueManagerName = queueManagerName;
			this.segEntriesQueueName = queueName;
			this.waitSegEntriesSeconds = waitSeconds;
			this.segEntriesHandler = segEntriesHandler;

			waitSegEntriesIsStopped = false;

			if ((waitSegEntriesThread == null)||(!waitSegEntriesThread.IsAlive)) // Must create new thread.
			{
				waitSegEntriesThread = new Thread(new ThreadStart(WaitSegEntries));
				waitSegEntriesThread.Name = "WaitSegEntries";
				waitSegEntriesThread.Start();

				Log.Write("Starting with new wait-SegEntries thread. [MqSeries.WaitSegEntriesStart(" + segEntriesQueueName + ")]", 2);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Starting with wait-SegEntries thread already running. [MqSeries.WaitSegEntriesStart(" + segEntriesQueueName + ")]", 2);
			}
		}

		public void WaitSegEntriesStop()
		{
			waitSegEntriesIsStopped = true;

			if (waitSegEntriesThread == null)
			{
				Log.Write("Stopping with wait-SegEntries thread never having started. [MqSeries.WaitSegEntriesStop]", 2);
			}
			else if (waitSegEntriesThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				waitSegEntriesThread.Abort();
				Log.Write("Sleeping wait-SegEntries thread has aborted. [MqSeries.WaitSegEntriesStop]", 2);
			}
			else
			{
				Log.Write("Stopping with wait-SegEntries thread still active. [MqSeries.WaitSegEntriesStop]", 2);
			}
		}

		private void WaitSegEntries()
		{
			MQQueueManager        queueManager = null;
			MQQueue               queue = null;
			MQMessage             message = null;
			MQGetMessageOptions   messageOptions = null;

			while (!waitSegEntriesIsStopped)
			{
				try 
				{
					if (queueManager == null)
					{
						queueManager = new MQQueueManager(segEntriesQueueManagerName);
					}

					queue = queueManager.AccessQueue(segEntriesQueueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_BROWSE + MQC.MQOO_FAIL_IF_QUIESCING);

					message = new MQMessage();
					messageOptions = new MQGetMessageOptions();
					messageOptions.Options = MQC.MQGMO_WAIT + MQC.MQGMO_BROWSE_FIRST + MQC.MQGMO_FAIL_IF_QUIESCING; 
					messageOptions.WaitInterval = waitSegEntriesSeconds * 1000;

					queue.Get(message, messageOptions); // This first message get is to peek, leaving the message on the queue if processing fails.
					byte[] messageBytes = new byte[message.TotalMessageLength];
					message.ReadFully(ref messageBytes, 0, message.TotalMessageLength);

					if (message.PutDateTime.ToString(Standard.DateFormat).CompareTo(Master.BizDateExchange) > 0)
					{
						Log.Write("Waiting for contracts to role to " + message.PutDateTime.ToString(Standard.DateFormat) + ". [MqSeries.WaitSegEntries]", 2);
						Thread.Sleep(delayOnBizDateRollSeconds * 1000);
						continue;
					}
					else if (message.PutDateTime.ToString(Standard.DateFormat).CompareTo(Master.BizDateExchange) < 0)
					{
						Log.Write("Discarding a message from " + message.PutDateTime.ToString(Standard.DateTimeFileFormat) + ". [MqSeries.WaitSegEntries]", 2);
						Log.Write(ascii.GetString(messageBytes) + ". [MqSeries.WaitSegEntries(" + segEntriesQueueName + ")]", 3);

						message = new MQMessage();
						messageOptions = new MQGetMessageOptions();
						messageOptions.Options = MQC.MQGMO_MSG_UNDER_CURSOR;
            
						queue.Get(message, messageOptions);
						continue;
					}
					else if (delayOnActivitySuspendSeconds > 0)
					{
						Log.Write("Suspending WaitSegEntries for " + delayOnActivitySuspendSeconds.ToString("#,##0") + " seconds. [MqSeries.WaitSegEntries]", 2);
						Thread.Sleep(delayOnActivitySuspendSeconds * 1000);
						continue;
					}

					if (segEntriesHandler(ascii.GetString(message.CorrelationId).Trim(), ascii.GetString(messageBytes))) // Now OK to pull message off the queue and discard.
					{ 
						Log.Write("New message, handler process status OK. [MqSeries.WaitSegEntries(" + segEntriesQueueName + ")]", 2);

						message = new MQMessage();
						messageOptions = new MQGetMessageOptions();
						messageOptions.Options = MQC.MQGMO_MSG_UNDER_CURSOR;
            
						queue.Get(message, messageOptions);
					}
					else
					{
						Log.Write("New message, handler process failed. [MqSeries.WaitSegEntries(" + segEntriesQueueName + ")]", Log.Error, 1);
						Thread.Sleep(delayOnQueueErrorSeconds * 1000);
					}
				}
				catch (MQException mqe) 
				{
					if (mqe.Reason != MQC.MQRC_NO_MSG_AVAILABLE)
					{
						queueManager = null;
						Log.Write(mqe.Message + " [MqSeries.WaitSegEntries(" + segEntriesQueueName + ")]", Log.Error, 1);
						Thread.Sleep(delayOnQueueErrorSeconds * 1000);
					}
					else
					{
						Log.Write("No new message. [MqSeries.WaitSegEntries(" + segEntriesQueueName + ")]", 3);
					}
				}
				catch (Exception e) 
				{
					Log.Write(e.Message + " [MqSeries.WaitSegEntries(" + segEntriesQueueName + ")]", Log.Error, 1);
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

		public void WaitTrmLockupStart(string queueManagerName, string queueName, int waitSeconds, TrmLockupHandlerDelegate trmLockupHandler)
		{
			this.trmLockupQueueManagerName = queueManagerName;
			this.trmLockupQueueName = queueName;
			this.waitTrmLockupSeconds = waitSeconds;
			this.trmLockupHandler = trmLockupHandler;

			waitTrmLockupIsStopped = false;

			if ((waitTrmLockupThread == null)||(!waitTrmLockupThread.IsAlive)) // Must create new thread.
			{
				waitTrmLockupThread = new Thread(new ThreadStart(WaitTrmLockup));
				waitTrmLockupThread.Name = "WaitTrmLockup";
				waitTrmLockupThread.Start();

				Log.Write("Starting with new wait-TrmLockup thread. [MqSeries.WaitTrmLockupStart(" + trmLockupQueueName + ")]", 2);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Starting with wait-TrmLockup thread already running. [MqSeries.WaitTrmLockupStart(" + trmLockupQueueName + ")]", 2);
			}
		}

		public void WaitTrmLockupStop()
		{
			waitTrmLockupIsStopped = true;

			if (waitTrmLockupThread == null)
			{
				Log.Write("Stopping with wait-TrmLockup thread never having started. [MqSeries.WaitTrmLockupStop]", 2);
			}
			else if (waitTrmLockupThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				waitTrmLockupThread.Abort();
				Log.Write("Sleeping wait-TrmLockup thread has aborted. [MqSeries.WaitTrmLockupStop]", 2);
			}
			else
			{
				Log.Write("Stopping with wait-TrmLockup thread still active. [MqSeries.WaitTrmLockupStop]", 2);
			}
		}

		private void WaitTrmLockup()
		{
			MQQueueManager        queueManager = null;
			MQQueue               queue = null;
			MQMessage             message = null;
			MQGetMessageOptions   messageOptions = null;

			while (!waitTrmLockupIsStopped)
			{
				try 
				{
					if (queueManager == null)
					{
						queueManager = new MQQueueManager(trmLockupQueueManagerName);
					}

					queue = queueManager.AccessQueue(trmLockupQueueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_BROWSE + MQC.MQOO_FAIL_IF_QUIESCING);

					message = new MQMessage();
					messageOptions = new MQGetMessageOptions();
					messageOptions.Options = MQC.MQGMO_WAIT + MQC.MQGMO_BROWSE_FIRST + MQC.MQGMO_FAIL_IF_QUIESCING; 
					messageOptions.WaitInterval = waitTrmLockupSeconds * 1000;

					queue.Get(message, messageOptions); // This first message get is to peek, leaving the message on the queue if processing fails.
					byte[] messageBytes = new byte[message.TotalMessageLength];
					message.ReadFully(ref messageBytes, 0, message.TotalMessageLength);

					if (message.PutDateTime.ToString(Standard.DateFormat).CompareTo(Master.BizDateExchange) > 0)
					{
						Log.Write("Waiting for contracts to role to " + message.PutDateTime.ToString(Standard.DateFormat) + ". [MqSeries.WaitTrmLockup]", 2);
						Thread.Sleep(delayOnBizDateRollSeconds * 1000);
						continue;
					}
					else if (message.PutDateTime.ToString(Standard.DateFormat).CompareTo(Master.BizDateExchange) < 0)
					{
						Log.Write("Discarding a message from " + message.PutDateTime.ToString(Standard.DateTimeFileFormat) + ". [MqSeries.WaitTrmLockup]", 2);
						Log.Write(ascii.GetString(messageBytes) + ". [MqSeries.WaitTrmLockup(" + trmLockupQueueName + ")]", 3);

						message = new MQMessage();
						messageOptions = new MQGetMessageOptions();
						messageOptions.Options = MQC.MQGMO_MSG_UNDER_CURSOR;
            
						queue.Get(message, messageOptions);
						continue;
					}
					else if (delayOnActivitySuspendSeconds > 0)
					{
						Log.Write("Suspending WaitTrmLockup for " + delayOnActivitySuspendSeconds.ToString("#,##0") + " seconds. [MqSeries.WaitTrmLockup]", 2);
						Thread.Sleep(delayOnActivitySuspendSeconds * 1000);
						continue;
					}

					if (trmLockupHandler(ascii.GetString(message.CorrelationId).Trim(), ascii.GetString(messageBytes))) // Now OK to pull message off the queue and discard.
					{ 
						Log.Write("New message, handler process status OK. [MqSeries.WaitTrmLockup(" + trmLockupQueueName + ")]", 2);

						message = new MQMessage();
						messageOptions = new MQGetMessageOptions();
						messageOptions.Options = MQC.MQGMO_MSG_UNDER_CURSOR;
            
						queue.Get(message, messageOptions);
					}
					else
					{
						Log.Write("New message, handler process failed. [MqSeries.WaitTrmLockup(" + trmLockupQueueName + ")]", Log.Error, 1);
						Thread.Sleep(delayOnQueueErrorSeconds * 1000);
					}
				}
				catch (MQException mqe) 
				{
					if (mqe.Reason != MQC.MQRC_NO_MSG_AVAILABLE)
					{
						queueManager = null;
						Log.Write(mqe.Message + " [MqSeries.WaitTrmLockup(" + trmLockupQueueName + ")]", Log.Error, 1);
						Thread.Sleep(delayOnQueueErrorSeconds * 1000);
					}
					else
					{
						Log.Write("No new message. [MqSeries.WaitTrmLockup(" + trmLockupQueueName + ")]", 3);
					}
				}
				catch (Exception e) 
				{
					Log.Write(e.Message + " [MqSeries.WaitTrmLockup(" + trmLockupQueueName + ")]", Log.Error, 1);
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

		public void WaitTrmReleaseStart(string queueManagerName, string queueName, int waitSeconds, TrmReleaseHandlerDelegate trmReleaseHandler)
		{
			this.trmReleaseQueueManagerName = queueManagerName;
			this.trmReleaseQueueName = queueName;
			this.waitTrmReleaseSeconds = waitSeconds;
			this.trmReleaseHandler = trmReleaseHandler;

			waitTrmReleaseIsStopped = false;

			if ((waitTrmReleaseThread == null)||(!waitTrmReleaseThread.IsAlive)) // Must create new thread.
			{
				waitTrmReleaseThread = new Thread(new ThreadStart(WaitTrmRelease));
				waitTrmReleaseThread.Name = "WaitTrmRelease";
				waitTrmReleaseThread.Start();

				Log.Write("Starting with new wait-TrmRelease thread. [MqSeries.WaitTrmReleaseStart(" + trmReleaseQueueName + ")]", 2);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Starting with wait-TrmRelease thread already running. [MqSeries.WaitTrmReleaseStart(" + trmReleaseQueueName + ")]", 2);
			}
		}

		public void WaitTrmReleaseStop()
		{
			waitTrmReleaseIsStopped = true;

			if (waitTrmReleaseThread == null)
			{
				Log.Write("Stopping with wait-TrmRelease thread never having started. [MqSeries.WaitTrmReleaseStop]", 2);
			}
			else if (waitTrmReleaseThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				waitTrmReleaseThread.Abort();
				Log.Write("Sleeping wait-TrmRelease thread has aborted. [MqSeries.WaitTrmReleaseStop]", 2);
			}
			else
			{
				Log.Write("Stopping with wait-TrmRelease thread still active. [MqSeries.WaitTrmReleaseStop]", 2);
			}
		}

		private void WaitTrmRelease()
		{
			MQQueueManager        queueManager = null;
			MQQueue               queue = null;
			MQMessage             message = null;
			MQGetMessageOptions   messageOptions = null;

			while (!waitTrmReleaseIsStopped)
			{
				try 
				{
					if (queueManager == null)
					{
						queueManager = new MQQueueManager(trmReleaseQueueManagerName);
					}

					queue = queueManager.AccessQueue(trmReleaseQueueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_BROWSE + MQC.MQOO_FAIL_IF_QUIESCING);

					message = new MQMessage();
					messageOptions = new MQGetMessageOptions();
					messageOptions.Options = MQC.MQGMO_WAIT + MQC.MQGMO_BROWSE_FIRST + MQC.MQGMO_FAIL_IF_QUIESCING; 
					messageOptions.WaitInterval = waitTrmReleaseSeconds * 1000;

					queue.Get(message, messageOptions); // This first message get is to peek, leaving the message on the queue if processing fails.
					byte[] messageBytes = new byte[message.TotalMessageLength];
					message.ReadFully(ref messageBytes, 0, message.TotalMessageLength);

					if (message.PutDateTime.ToString(Standard.DateFormat).CompareTo(Master.BizDateExchange) > 0)
					{
						Log.Write("Waiting for contracts to role to " + message.PutDateTime.ToString(Standard.DateFormat) + ". [MqSeries.WaitTrmRelease]", 2);
						Thread.Sleep(delayOnBizDateRollSeconds * 1000);
						continue;
					}
					else if (message.PutDateTime.ToString(Standard.DateFormat).CompareTo(Master.BizDateExchange) < 0)
					{
						Log.Write("Discarding a message from " + message.PutDateTime.ToString(Standard.DateTimeFileFormat) + ". [MqSeries.WaitTrmRelease]", 2);
						Log.Write(ascii.GetString(messageBytes) + ". [MqSeries.WaitTrmRelease(" + trmReleaseQueueName + ")]", 3);

						message = new MQMessage();
						messageOptions = new MQGetMessageOptions();
						messageOptions.Options = MQC.MQGMO_MSG_UNDER_CURSOR;
            
						queue.Get(message, messageOptions);
						continue;
					}
					else if (delayOnActivitySuspendSeconds > 0)
					{
						Log.Write("Suspending WaitTrmRelease for " + delayOnActivitySuspendSeconds.ToString("#,##0") + " seconds. [MqSeries.WaitTrmRelease]", 2);
						Thread.Sleep(delayOnActivitySuspendSeconds * 1000);
						continue;
					}

					if (trmReleaseHandler(ascii.GetString(message.CorrelationId).Trim(), ascii.GetString(messageBytes))) // Now OK to pull message off the queue and discard.
					{ 
						Log.Write("New message, handler process status OK. [MqSeries.WaitTrmRelease(" + trmReleaseQueueName + ")]", 2);

						message = new MQMessage();
						messageOptions = new MQGetMessageOptions();
						messageOptions.Options = MQC.MQGMO_MSG_UNDER_CURSOR;
            
						queue.Get(message, messageOptions);
					}
					else
					{
						Log.Write("New message, handler process failed. [MqSeries.WaitTrmRelease(" + trmReleaseQueueName + ")]", Log.Error, 1);
						Thread.Sleep(delayOnQueueErrorSeconds * 1000);
					}
				}
				catch (MQException mqe) 
				{
					if (mqe.Reason != MQC.MQRC_NO_MSG_AVAILABLE)
					{
						queueManager = null;
						Log.Write(mqe.Message + " [MqSeries.WaitTrmRelease(" + trmReleaseQueueName + ")]", Log.Error, 1);
						Thread.Sleep(delayOnQueueErrorSeconds * 1000);
					}
					else
					{
						Log.Write("No new message. [MqSeries.WaitTrmRelease(" + trmReleaseQueueName + ")]", 3);
					}
				}
				catch (Exception e) 
				{
					Log.Write(e.Message + " [MqSeries.WaitTrmRelease(" + trmReleaseQueueName + ")]", Log.Error, 1);
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
            MQC.MQOO_FAIL_IF_QUIESCING);
        }
        else
        {
          queue = queueManager.AccessQueue(queueName,
            MQC.MQOO_OUTPUT +
            MQC.MQOO_FAIL_IF_QUIESCING,
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
        messageOptions.Options = MQC.MQGMO_NO_SYNCPOINT;

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
