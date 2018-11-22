using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Sensors
{
    public class TemperatureSensor: Sensor
    {

        public TemperatureSensor(): base("TMP-01", "Temperature", "°C")
        {
            MessageValue = getMeasure();
            
        }

        public override decimal MessageValue { get; set; }

        public override decimal getMeasure()
        {
            Random rnd = new Random();
            MessageValue = new decimal((double)rnd.Next(10, 500) / 10);
            return MessageValue;
        }

        public override object exposeValue()
        {
            

            object measure = new
            {
                base.Id,
                base.SensorName,
                MessageValue,
                MeasureName,
                UnitOfMeasure,
                Timestamp
            };

            return JsonConvert.SerializeObject(measure);
        }

        public override void setMeasureValue(decimal message)
        {
            MessageValue = message;
        }
    }
}


/*
public override Guid Id { get; set; }
public override string SensorName { get; set; }
public override decimal MessageValue { get; set; }
public override string MeasureName { get; set; }
public override string UnitOfMeasure { get; set; }
public override double TimeStamp
{
get { return TimeStamp; }
set
{
var epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
value = epoch;
}
}


public TemperatureSensor(string sensorName, string measureName)
{
SensorName = sensorName;
MeasureName = measureName;
Id = new Guid();
}

public override decimal getMeasure()
{
return MessageValue;
}

public override object SendValue(ISensorsManager o)
{
var id = o.Id.ToString();

object sensor = new
{
id,
o.SensorName,
o.MessageValue,
o.MeasureName,
o.UnitOfMeasure,
o.TimeStamp
};

return JsonConvert.SerializeObject(sensor);
}

public override void setMeasureValue(decimal message)
{
MessageValue = message;
}*/
