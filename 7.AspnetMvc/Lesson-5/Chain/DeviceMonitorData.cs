using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chain
{
    public class DeviceMonitorData : IMonitorData
    {
        public int Cpu { get; set; }
        public int Memory { get; set; }
        public string Name { get; set; } = "";
    }
}