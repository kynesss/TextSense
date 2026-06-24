using System.Text.Json.Serialization;

namespace TextSense.Models.OpenAi;

/// <summary>Request body for the OpenAI chat completions endpoint.</summary>
public sealed record ChatCompletionRequest
{
    [JsonPropertyName("model")]
    public required string Model { get; init; }

    [JsonPropertyName("messages")]
    public required IReadOnlyList<ChatMessage> Messages { get; init; }

    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; init; }
}
