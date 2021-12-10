using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chain
{
    public sealed class MemoryMonitorPipelineItem : MonitorPipeLineItem
    {
        public override bool Proceed(IMonitorData data)
        {
            if (data is null)
                return false;

            if (data.Memory < 10)
                return false;

            Console.WriteLine($"{data.Name}: Обрабатываем данные Memory {data.Memory}");
            return true;
        }
    }
}