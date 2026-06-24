using System.Text.Json.Serialization;

namespace TextSense.Models.OpenAi;

public sealed record ChatCompletionResponse
{
    [JsonPropertyName("choices")]
    public IReadOnlyList<ChatChoice>? Choices { get; init; }

    [JsonPropertyName("usage")]
    public ChatUsage? Usage { get; init; }
}

public sealed record ChatChoice
{
    [JsonPropertyName("message")]
    public ChatMessage? Message { get; init; }

    [JsonPropertyName("finish_reason")]
    public string? FinishReason { get; init; }
}

public sealed record ChatUsage
{
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; init; }

    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; init; }

    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; init; }
}
