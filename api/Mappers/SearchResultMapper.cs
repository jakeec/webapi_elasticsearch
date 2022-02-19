using api.Domain;
using Nest;

namespace api.Mappers;

public static class SearchResponseExtensions
{
    private static ISuggestionOption ToNewsHeadlineSuggestionOption(this ISuggestOption<NewsHeadline> suggestOption) {
        return new SuggestionOption {
            Text = suggestOption.Text,
            Score = suggestOption.Score,
            Frequency = suggestOption.Frequency
        };
    }

    private static IList<ISuggestionOption> ToNewsHeadlineSuggestionsOptions(this IReadOnlyCollection<ISuggestOption<NewsHeadline>> options) {
        return options.Select(ToNewsHeadlineSuggestionOption).ToList<ISuggestionOption>();
    }

    public static ISearchResult<NewsHeadline> ToNewsHeadlineSearchResult(this ISearchResponse<NewsHeadline> searchResponse)
    {
        return new SearchResult<NewsHeadline>
        {
            IsValid = searchResponse.IsValid,
            Suggestions = searchResponse.Suggest.Values.First().Select(s => new Suggestion
            {
                Text = s.Text,
                Offset = s.Offset,
                Length = s.Length,
                Options = s.Options.ToNewsHeadlineSuggestionsOptions()
            }).ToList<ISuggestion>(),
            Hits = searchResponse.Documents.ToList<NewsHeadline>(),
        };
    }
}