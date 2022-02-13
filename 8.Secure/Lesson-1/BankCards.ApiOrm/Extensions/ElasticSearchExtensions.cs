using BankCards.ApiOrm.Configuration;
using BankCards.DAL.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Npgsql;
using System.Data.Common;
using Elasticsearch.Net;
using Nest;

namespace BankCards.ApiOrm.Extensions;

public static class ElasticSearchExtensions
{
    public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration config)
    {
        var elasticSettings = new ElasticSettings();
        config.GetSection("ElasticSettings").Bind(elasticSettings);
        var pool = new SingleNodeConnectionPool(new Uri(elasticSettings.ConnectionString));
        var settings = new ConnectionSettings(pool)
            .DefaultIndex(elasticSettings.Index);
        var client = new ElasticClient(settings);
        services.AddSingleton(client);

        return services;
    }
}
