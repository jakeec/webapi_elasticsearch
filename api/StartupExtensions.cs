using Elasticsearch.Net;
using Nest;

namespace api.StartupExtensions;

public static class ElasticClientExtensions
{
    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
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

        var elasticClient = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(elasticClient);
    }
}