namespace TextSense.Services;

public sealed record SentimentScores(double Positive, double Neutral, double Negative);

public sealed record TextAnalysis
{
    public required string Sentiment { get; init; }
    public SentimentScores? Scores { get; init; }
    public required IReadOnlyList<string> KeyPhrases { get; init; }
}
