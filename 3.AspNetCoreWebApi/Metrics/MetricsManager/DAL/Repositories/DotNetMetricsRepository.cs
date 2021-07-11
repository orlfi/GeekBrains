﻿using System;
using System.Collections.Generic;
using Core.Interfaces;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System.Linq;
using Dapper;

namespace MetricsManager.DAL.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly IConnectionManager _connectionManager;

        public DotNetMetricsRepository(IConnectionManager connectionManager) => _connectionManager = connectionManager;

        public void Create(DotNetMetric item)
        {
            var connection = _connectionManager.GetOpenedConnection();

            connection.Execute("INSERT INTO DotNetMetrics(Value, Time) VALUES (@Value, @Time)",
                new
                {
                    item.Value,
                    item.Time
                });
        }

        public IList<DotNetMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<DotNetMetric>("SELECT Id, Value, Time FROM DotNetMetrics WHERE Time >= @FromTime AND Time <= @ToTime",
                new
                {
                    FromTime = fromTime,
                    ToTime = toTime
                }).ToList();

            return result;
        }

        public IList<DotNetMetric> GetByPeriodFormAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = _connectionManager.GetOpenedConnection();

            var result = connection.Query<DotNetMetric>("SELECT Id, AgentId, Value, Time FROM DotNetMetric WHERE Time >= @FromTime AND Time <= @ToTime AND AgentId = @AgentId",
                new
                {
                    AgentId = agentId,
                    FromTime = fromTime,
                    ToTime = toTime
                }).ToList();

            return result;
        }
    }
}
