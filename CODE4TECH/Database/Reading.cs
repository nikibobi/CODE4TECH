using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CODE4TECH.Database
{
    public class Reading
    {
        public int DeviceId { get; set; }
        public DateTime Time { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
