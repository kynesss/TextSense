using System.Text.Json.Serialization;

namespace TextSense.Models.AzureLanguage;

/// <summary>Request body for the Azure AI Language "analyze-text" endpoint.</summary>
public sealed record AnalyzeTextRequest
{
    [JsonPropertyName("kind")]
    public required string Kind { get; init; }

    [JsonPropertyName("analysisInput")]
    public required AnalysisInput AnalysisInput { get; init; }

    [JsonPropertyName("parameters")]
    public IReadOnlyDictionary<string, object>? Parameters { get; init; }
}

/// <summary>Wrapper holding the documents to analyze.</summary>
public sealed record AnalysisInput
{
    [JsonPropertyName("documents")]
    public required IReadOnlyList<AnalysisDocument> Documents { get; init; }
}

/// <summary>A single document submitted for analysis.</summary>
public sealed record AnalysisDocument
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("language")]
    public required string Language { get; init; }

    [JsonPropertyName("text")]
    public required string Text { get; init; }
}
