using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chain
{
    public interface IMonitorPipeLineItem
    {
        void Next(IMonitorPipeLineItem next);

        void ProcessData(IMonitorData data);
    }
}