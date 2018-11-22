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
    /// <summary>
    /// Fornisce metodi per esporre i dati dalla macchina
    /// </summary>
    public class CarManager
    {

        /// <summary>
        /// Esegue una post alle API inviando i dati presenti nella <paramref name="queue"/>
        /// </summary>
        /// <param name="queue">Coda da dove andare a recuperare i dati</param>
        public void ExecuteHTTPTelemetry(QueueManager queue)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/v1/sensors/write");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            object responseObj = new object();
            try
            {

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var msg = queue.ReadmMSMQQueue(); //leggo dalla cosa
                    streamWriter.Write(msg);

                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //responseObj = httpResponse.StatusCode.ToString();
                Console.Out.WriteLine(httpResponse.StatusCode);
            }
            catch (Exception e)
            {
                Console.WriteLine($"NO CONNECTION");
            }
        }


    }
}

