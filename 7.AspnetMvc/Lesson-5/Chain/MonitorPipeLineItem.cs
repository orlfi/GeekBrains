using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chain
{
    public abstract class MonitorPipeLineItem : IMonitorPipeLineItem
    {
        private IMonitorPipeLineItem? _next = null;

        public void Next(IMonitorPipeLineItem next)
        {
            _next = next;
        }

        public void ProcessData(IMonitorData data)
        {

            if (Proceed(data))
            {
                _next?.ProcessData(data);
            }
        }

        public abstract bool Proceed(IMonitorData data);
    }
}