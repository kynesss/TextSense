using System.Text.Json.Serialization;

namespace TextSense.Models.AzureLanguage;

public sealed record AnalyzeTextResponse
{
    [JsonPropertyName("kind")]
    public string? Kind { get; init; }

    [JsonPropertyName("results")]
    public AnalyzeTextResults? Results { get; init; }
}

public sealed record AnalyzeTextResults
{
    [JsonPropertyName("documents")]
    public IReadOnlyList<AnalyzeTextDocument>? Documents { get; init; }

    [JsonPropertyName("errors")]
    public IReadOnlyList<AnalyzeTextError>? Errors { get; init; }

    [JsonPropertyName("modelVersion")]
    public string? ModelVersion { get; init; }
}

public sealed record AnalyzeTextDocument
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("sentiment")]
    public string? Sentiment { get; init; }

    [JsonPropertyName("confidenceScores")]
    public ConfidenceScores? ConfidenceScores { get; init; }

    [JsonPropertyName("keyPhrases")]
    public IReadOnlyList<string>? KeyPhrases { get; init; }
}

public sealed record ConfidenceScores
{
    [JsonPropertyName("positive")]
    public double Positive { get; init; }

    [JsonPropertyName("neutral")]
    public double Neutral { get; init; }

    [JsonPropertyName("negative")]
    public double Negative { get; init; }
}

public sealed record AnalyzeTextError
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("error")]
    public ErrorDetails? Error { get; init; }
}

public sealed record ErrorDetails
{
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }
}
