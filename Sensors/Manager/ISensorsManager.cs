using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Sensors
{
    public interface ISensorsManager
    {
        void setMeasureValue(decimal message);
        decimal getMeasure();
        object exposeValue();
 
    }
}
