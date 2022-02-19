namespace api.Services;

using Nest;

using api.Domain;
using api.Mappers;

public interface ISearchEngine
{
    Task<ISearchResult<NewsHeadline>> SearchAsync(string searchTerm);
}

public class ElasticsearchSearchEngine : ISearchEngine
{
    private readonly IElasticClient _elasticClient;
    private readonly ILogger<ElasticsearchSearchEngine> _logger;

    public ElasticsearchSearchEngine(IElasticClient elasticClient, ILogger<ElasticsearchSearchEngine> logger)
    {
        _elasticClient = elasticClient;
        _logger = logger;
    }

    public async Task<ISearchResult<NewsHeadline>> SearchAsync(string searchTerm)
    {
        var searchRequest = new SearchRequest<NewsHeadline>()
        {
            Query = new MatchQuery
            {
                Field = Infer.Field<NewsHeadline>(n => n.ShortDescription),
                Query = searchTerm,
                Operator = Operator.Or,
                Lenient = true
            },
            Suggest = new SuggestContainer {
                {
                    "term-suggester", new SuggestBucket {
                        Text = searchTerm,
                        Term = new TermSuggester {
                            MaxEdits = 2,
                            Field = Infer.Field<NewsHeadline>(n => n.ShortDescription)
                        }
                    }
                }
            }
        };
        var searchResponse = await _elasticClient.SearchAsync<NewsHeadline>(searchRequest);
        return searchResponse.ToNewsHeadlineSearchResult();
    }
}