namespace api.Services;

using System.Collections.Generic;
using api.Domain;
using Elasticsearch.Net;
using Nest;

public interface ISearchResult<T>
{
    bool IsValid { get; set; }
    IList<string> Suggestions { get; set; }
    IList<T> Hits { get; set; }
}

public class SearchResult<T> : ISearchResult<T>
{
    public bool IsValid { get; set; }
    public IList<string> Suggestions { get; set; }
    public IList<T> Hits { get; set; }
}

public interface ISearchEngine
{
    Task<ISearchResult<NewsHeadline>> SearchAsync(string searchTerm);
}

public class ElasticsearchSearchEngine : ISearchEngine
{
    private readonly IConfiguration _configuration;
    private readonly ElasticClient _elasticClient;

    public ElasticsearchSearchEngine(IConfiguration configuration)
    {
        _configuration = configuration;
        _elasticClient = InitialiseElasticClient(_configuration);
    }

    private ElasticClient InitialiseElasticClient(IConfiguration configuration)
    {
        string username = configuration["ElasticClient:Username"];
        string password = configuration["ElasticClient:Password"];
        string defaultIndex = configuration["ElasticClient:DefaultIndex"];
        Uri uri = new(configuration["ElasticClient:Uri"]);

        var settings = new ConnectionSettings(uri)
            .DefaultIndex(defaultIndex)
            .BasicAuthentication(username, password)
            .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
            .DisableDirectStreaming();
        return new ElasticClient(settings);
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
            }
        };
        var searchResponse = await _elasticClient.SearchAsync<NewsHeadline>(searchRequest);
        return new SearchResult<NewsHeadline>
        {
            IsValid = searchResponse.IsValid,
            Hits = searchResponse.Documents.ToList<NewsHeadline>(),
        };
    }
}