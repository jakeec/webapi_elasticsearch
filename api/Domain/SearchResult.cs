namespace api.Domain;

public interface ISearchResult<T>
{
    bool IsValid { get; set; }
    IList<ISuggestion> Suggestions { get; set; }
    IList<T> Hits { get; set; }
}

public interface ISuggestionOption {
    string Text { get; set; }
    double Score { get; set; }
    long Frequency { get; set; }
}

public interface ISuggestion
{
    string Text { get; set; }
    int Offset { get; set; }
    int Length { get; set; }
    IList<ISuggestionOption> Options { get; set; }
}

public class SuggestionOption : ISuggestionOption {
    public string Text { get; set; }
    public double Score { get; set; }
    public long Frequency { get; set; }
}

public class Suggestion : ISuggestion {
    public string Text { get; set; }
    public int Offset { get; set; }
    public int Length { get; set; }
    public IList<ISuggestionOption> Options { get; set; }
}

public class SearchResult<T> : ISearchResult<T>
{
    public bool IsValid { get; set; }
    public IList<ISuggestion> Suggestions { get; set; }
    public IList<T> Hits { get; set; }
}