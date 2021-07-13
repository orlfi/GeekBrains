﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Core.Responses;
using MetricsAgent.DAL.Interfaces;

using AutoMapper;

namespace MetricsAgent.Features.Queries
{
    public class CpuMetricGetByPeriodQuery : IRequest<AgentCpuMetricResponse>
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class CpuMetricGetByPeriodQueryHandler : IRequestHandler<CpuMetricGetByPeriodQuery, AgentCpuMetricResponse>
        {
            private readonly ICpuMetricsRepository _repository;
            private readonly IMapper _mapper;

            public CpuMetricGetByPeriodQueryHandler(ICpuMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<AgentCpuMetricResponse> Handle(CpuMetricGetByPeriodQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriod(request.FromTime, request.ToTime);

                    var response = new AgentCpuMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<AgentCpuMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
