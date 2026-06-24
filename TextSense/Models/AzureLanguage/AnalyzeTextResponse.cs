using System.Text.Json.Serialization;

namespace TextSense.Models.AzureLanguage;

/// <summary>Response returned by the Azure AI Language "analyze-text" endpoint.</summary>
public sealed record AnalyzeTextResponse
{
    [JsonPropertyName("kind")]
    public string? Kind { get; init; }

    [JsonPropertyName("results")]
    public AnalyzeTextResults? Results { get; init; }
}

/// <summary>The results payload containing per-document outcomes.</summary>
public sealed record AnalyzeTextResults
{
    [JsonPropertyName("documents")]
    public IReadOnlyList<AnalyzeTextDocument>? Documents { get; init; }

    [JsonPropertyName("errors")]
    public IReadOnlyList<AnalyzeTextError>? Errors { get; init; }

    [JsonPropertyName("modelVersion")]
    public string? ModelVersion { get; init; }
}

/// <summary>Analysis outcome for a single document.</summary>
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

/// <summary>Per-sentiment confidence scores (0..1).</summary>
public sealed record ConfidenceScores
{
    [JsonPropertyName("positive")]
    public double Positive { get; init; }

    [JsonPropertyName("neutral")]
    public double Neutral { get; init; }

    [JsonPropertyName("negative")]
    public double Negative { get; init; }
}

/// <summary>Error reported for a single document.</summary>
public sealed record AnalyzeTextError
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("error")]
    public ErrorDetails? Error { get; init; }
}

/// <summary>Details of an analysis error.</summary>
public sealed record ErrorDetails
{
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }
}
