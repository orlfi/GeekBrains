using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chain
{
    public interface IMonitorData
    {
        public string Name { get; set; }

        public int Cpu { get; set; }

        public int Memory { get; set; }
    }


}