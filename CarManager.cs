using Client.Sensors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Client
{
    public class CarManager
    {
        public void ExecuteHTTPTelemetry(List<Sensor> allSensors)
        {
            QueueManager queueManager;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/v1/sensors/write");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";



            foreach (var sensor in allSensors)
            {
                queueManager = new QueueManager("carData", sensor.exposeValue(), "dati rilevati da ICars");
                try
                {
                    //var obj = queueManager.ReadMSMQQueue();
                    //queueManager.ReadMSMQQueueList();
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        
                        streamWriter.Write(sensor.exposeValue());
                        //streamWriter.Write(JsonConvert.SerializeObject(obj));
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    Console.Out.WriteLine(httpResponse.StatusCode);
                }
                catch (Exception e)
                {

                    queueManager.WriteMSMQ();
                    Console.WriteLine($"Aggiunto alla coda: {sensor.exposeValue()}");
                }

            }

        }

    
    }
}

