﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MetricsManager.Features.Queries.Metrics;
using MediatR;

namespace MetricsManager.Controllers
{

    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IMediator _mediator;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

            _logger.LogDebug(1, "Logger dependency injected to DotNetMetricsController");
        }

        /// <summary>
        /// Gets DotNet metrics on a given time range
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/metrics/dotnet/agent/1/heap-size/from/2021-07-01/to/2021-07-10
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpGet("agent/{agentId}/heap-size/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] DotNetMetricGetByPeriodFromAgentQuery request)
        {
            _logger.LogInformation($"DotNet GetMetricsFromAgent Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Gets DotNet metrics on a given time range from all agents
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/metrics/dotnet/cluster/from/2021-07-01/to/2021-07-10
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">If everything is ok</response>
        [HttpGet("cluster/heap-size/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] DotNetMetricGetByPeriodQuery request)
        {
            _logger.LogInformation($"DotNet GetMetricsFromAllCluster Parameters: {request}");

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
