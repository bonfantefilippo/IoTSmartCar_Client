using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class QueueManager
    {
        private string QueueName { get; set; }
        private object Message { get; set; }
        private object Description { get; set; }
        private string Path { get; set; }

        public QueueManager(string queueName, object message, object description)
        {
            QueueName = queueName;
            Message = message;
            Description = description;
            Path = $@".\Private$\{QueueName}";
        }

        public void WriteMSMQ()
        {

            //string description = "test queue", message = "test message", path = @".\Private$\IDG";
            // string path = $@".\Private$\{QueueName}";
            MessageQueue messageQueue = null;
            try
            {
                if (MessageQueue.Exists(Path))
                {
                    messageQueue = new MessageQueue(Path);
                    messageQueue.Label = Description.ToString();
                }
                else
                {
                    MessageQueue.Create(Path);
                    messageQueue = new MessageQueue(Path);
                    messageQueue.Label = Description.ToString();
                }
                messageQueue.Send(Message);
            }
            catch
            {
                throw;
            }
            finally
            {
                messageQueue.Dispose();
            }

        }

        public object ReadMSMQQueue()
        {
            //string path = $@".\Private$\{QueueName}";
            Message message;

            using (MessageQueue messageQueue = new MessageQueue(Path))
            {
                    message = messageQueue.Receive();
                   
                    message.Formatter = new XmlMessageFormatter(new string[] { "System.String, mscorlib" });
                    object msg = message.Body;
                    
                    Console.WriteLine($"Reading: {msg}");
                
            }
            return message;
        }

        public List<object> ReadMSMQQueueList()
        {
          
            List<object> lstMessages = new List<object>();
            Message[] messages;
      

            using (MessageQueue messageQueue = new MessageQueue(Path))
            {
                
                messages = new[] { messageQueue.Receive() };
                //messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(asyncResult.AsyncResult);
                foreach(Message msgQueue in messages)
                {
                    msgQueue.Formatter = new XmlMessageFormatter(new string[] { "System.String, mscorlib" });
                    object msg = msgQueue.Body;
                    lstMessages.Add(msg);
                    Console.WriteLine($"Reading: {msg}");
                }
               

            }
            return lstMessages;
        }

        /*private async Task<Message> MyAsincReceive()
        {
            var queue = new MessageQueue(Path);
            var message = await Task.Factory.FromAsync(queue.BeginPeek(), queue.EndPeek);

        }*/
       
    }
}
