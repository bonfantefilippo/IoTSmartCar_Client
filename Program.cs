using System;
using System.Collections.Generic;
using Client.Sensors;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Messaging;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TemperatureSensor temperatureSensor = new TemperatureSensor();
            
            CarManager carManager = new CarManager();
            List<Sensor> sensori = new List<Sensor>
                {
                    temperatureSensor
                };

            QueueManager carQueue = new QueueManager("carData", sensori, "Data from SmartCar");
            carQueue.ReadmMSMQQueue();
            while (true)
            {
                temperatureSensor.getMeasure(); //imposta la misura al sensore

                carQueue.WriteOnMSMQ(); //scrivo nella coda MSMQ
                carManager.ExecuteHTTPTelemetry(carQueue);
                System.Threading.Thread.Sleep(1000);

            }

        }

    }

}






/*

// init sensors
TemperatureSensorInterface temperatureSensor = new VirtualTemperatureSensor();
// TODO add more sensors
TemperatureSensor tp = new TemperatureSensor();
tp.setMeasureValue(10);


HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/v1/sensors/write");
httpWebRequest.ContentType = "text/json";
httpWebRequest.Method = "POST";


using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
{
    streamWriter.Write(tp.exposeValue(tp));
   // Console.WriteLine(JsonConvert.SerializeObject(tp.SendValue(tp)));
}

var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

Console.Out.WriteLine(httpResponse.StatusCode);*/
