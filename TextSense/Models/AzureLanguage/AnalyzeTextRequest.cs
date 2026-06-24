using System.Text.Json.Serialization;

namespace TextSense.Models.AzureLanguage;

public sealed record AnalyzeTextRequest
{
    [JsonPropertyName("kind")]
    public required string Kind { get; init; }

    [JsonPropertyName("analysisInput")]
    public required AnalysisInput AnalysisInput { get; init; }

    [JsonPropertyName("parameters")]
    public IReadOnlyDictionary<string, object>? Parameters { get; init; }
}

public sealed record AnalysisInput
{
    [JsonPropertyName("documents")]
    public required IReadOnlyList<AnalysisDocument> Documents { get; init; }
}

public sealed record AnalysisDocument
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("language")]
    public required string Language { get; init; }

    [JsonPropertyName("text")]
    public required string Text { get; init; }
}
