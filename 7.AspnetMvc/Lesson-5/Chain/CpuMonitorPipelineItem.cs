using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chain
{
    public sealed class CpuMonitorPipelineItem : MonitorPipeLineItem
    {
        public override bool Proceed(IMonitorData data)
        {
            if (data is null)
                return false;

            if (data.Cpu == 0)
                return false;

            Console.WriteLine($"{data.Name} Обрабатываем данные CPU {data.Cpu}");
            return true;
        }
    }
}