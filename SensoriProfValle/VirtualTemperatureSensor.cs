using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Sensors
{
    class VirtualTemperatureSensor : TemperatureSensorInterface
    {
        public void setTemperature(decimal temperature)
        { }

        public decimal getTemperature()
        {
            Random random = new Random();
            return new decimal((double) random.Next(10, 500) / 10);
        }

        public string toJson()
        {
            return "{\"temperature\":\"" + getTemperature() + "\"}";
        }
    }
}
