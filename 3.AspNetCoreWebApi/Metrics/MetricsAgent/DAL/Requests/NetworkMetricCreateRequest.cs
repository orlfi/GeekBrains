﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Requests
{
    public class NetworkMetricCreateRequest
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }

        public override string ToString()
        {
            return $"{{Value={Value}, Time={Time}}}";
        }
    }
}