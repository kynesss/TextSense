using System.Text.Json.Serialization;

namespace TextSense.Models.OpenAi;

public sealed record ChatMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);
