﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class CpuMetricResponse
    {
        public List<CpuMetricDTO> Metrics { get; set; } = new List<CpuMetricDTO>();
    }
}