using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Sensors
{
    public abstract class Sensor: ISensorsManager
    {
        public Guid Id { get; set; }
        public string SensorName { get; set; }
        public abstract decimal MessageValue { get; set; }
        public string MeasureName { get; set; }
        public string UnitOfMeasure { get; set; }
        public double Timestamp { get; set; }

        public Sensor(string sensorName, string measureName, string unitOfMeasure)
        {
            SensorName = sensorName;
            MeasureName = measureName;
            UnitOfMeasure = unitOfMeasure;
            Id = Guid.NewGuid();
            Timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        public abstract void setMeasureValue(decimal message);

        public abstract decimal getMeasure();

        public abstract object exposeValue();
    }
}
