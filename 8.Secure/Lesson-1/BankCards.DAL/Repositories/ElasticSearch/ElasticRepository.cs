using BankCards.Domain.Mongo;
using BankCards.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Nest;

namespace BankCards.DAL.Repositories.ElasticSearch;

public class ElasticRepository : IElasticRepository
{
    private readonly ElasticClient _client;
    private readonly ILogger<ElasticRepository> _logger;

    public ElasticRepository(ElasticClient client, ILogger<ElasticRepository> logger)
    {
        _client = client;
        _logger = logger;
    }

    public IEnumerable<Book> Search(string query)
    {
        ISearchResponse<Book> result;
        var aa = _client.Indices;
        if (!string.IsNullOrWhiteSpace(query))
        {

            result = _client.Search<Book>(s => s.Source()
                .Query(q => q
                .QueryString(qs => qs
                   .Query(query)
                   .Fields(fs => fs
                       .Fields(f1 => f1.LongDescription,
                               f2 => f2.ShortDescription
                               )
                   )
                   )));
        }
        else
        {
            result = _client.Search<Book>(s => s
                .Query(q => q
                    .MatchAll()
                ).Size(1000)
            );
        }

        return result.Documents.ToArray();
    }
}
