using BankCards.Domain.Mongo;
using BankCards.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Nest;
using System.Text;

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
               .Query(qry => qry
                   .QueryString(qs => qs
                       .Query(query)
                       .Fields(fs => fs
                           .Fields(f1 => f1.LongDescription)
                        )
                    )
                )
                .Size(1000)
                .Highlight(h => h
                    .PreTags("<b>")
                    .PostTags("</b>")
                    .Fields(
                    fs => fs
                        .Field(p => p.LongDescription)
                        .PreTags("<b>")
                        .PostTags("</b>")
                    )
                )
            );
        }
        else
        {
            result = _client.Search<Book>(s => s
                .Query(q => q
                    .MatchAll()
                ).Size(1000)
            );
        }

        return ReplaceHighlights(result);
    }

    private IEnumerable<Book> ReplaceHighlights(ISearchResponse<Book> response)
    {
        List<Book> result = new List<Book>();
        foreach (var hit in response.Hits)
        {
            StringBuilder sb = new StringBuilder(hit.Source.LongDescription);
            foreach (var highlight in hit.Highlight)
            {

                foreach (var val in highlight.Value)
                {
                    sb.Replace(val.Replace("<b>", "").Replace("</b>", ""), val);
                }
            }
            result.Add(new Book()
            {
                Title = hit.Source.Title,
                LongDescription = sb.ToString(),
                ShortDescription = hit.Source.ShortDescription,
            });
        }
        return result;
    }
}
