using System.Text.Json.Serialization;

namespace TextSense.Models.OpenAi;

/// <summary>A single message in an OpenAI chat completion exchange.</summary>
public sealed record ChatMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);
