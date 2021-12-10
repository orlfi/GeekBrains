using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chain
{
    public sealed class MonitorDevicePipeline
    {
        private IMonitorData _data;

        public MonitorDevicePipeline(IMonitorData data)
        {
            _data = data;
        }

        public void RunPipeline()
        {
            var pipelineItem = CreatePipeline();

            pipelineItem.ProcessData(_data);
        }

        private IMonitorPipeLineItem CreatePipeline()
        {
            var cpuMonitorPipelineItem = new CpuMonitorPipelineItem();
            var memoryMonitorPipelineItem = new MemoryMonitorPipelineItem();
            cpuMonitorPipelineItem.Next(memoryMonitorPipelineItem);
            return cpuMonitorPipelineItem;
        }

    }
}