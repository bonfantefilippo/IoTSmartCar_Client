using Client.Sensors;
using Newtonsoft.Json;
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
        private List<Sensor> Data { get; set; }
        private object Description { get; set; }
        public string Path { get; set; }


        public QueueManager(string queueName, List<Sensor> data, object description)
        {
            QueueName = queueName;
            Data = data;
            Description = description;
            Path = $@".\Private$\{QueueName}";
        }
        public QueueManager()
        {

        }

        public void WriteOnMSMQ()
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

                foreach (var ms in Data)
                {
                    messageQueue.Send(ms.exposeValue());
                    Console.WriteLine($"Aggiunto alla coda: {ms.exposeValue()}");
                }
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


        /// <summary>
        /// Legge dalla coda MSMQ
        /// </summary>
        /// <returns>Oggetto contente il messaggio nella <see cref="QueueName"/></returns>
        public object ReadmMSMQQueue()
        {
            Message message;
            object msg = new object();
            using (MessageQueue messageQueue = new MessageQueue(Path))
            {

                try
                {
                    message = messageQueue.Receive();
                    //message.Formatter = new XmlMessageFormatter(new Type[] { typeof(Sensor) });
                    message.Formatter = new XmlMessageFormatter(new string[] { "System.String, mscorlib" });
                    msg = message.Body;

                    //Console.WriteLine($"Reading: {msg}");
                }
                catch (MessageQueueException e)
                {
                    Console.WriteLine($"Errore: {e.Message}");
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }
            return msg;
        }
    }
}
