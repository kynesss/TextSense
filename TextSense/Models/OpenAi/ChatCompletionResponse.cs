using System.Text.Json.Serialization;

namespace TextSense.Models.OpenAi;

/// <summary>Response returned by the OpenAI chat completions endpoint.</summary>
public sealed record ChatCompletionResponse
{
    [JsonPropertyName("choices")]
    public IReadOnlyList<ChatChoice>? Choices { get; init; }

    [JsonPropertyName("usage")]
    public ChatUsage? Usage { get; init; }
}

/// <summary>A single generated choice.</summary>
public sealed record ChatChoice
{
    [JsonPropertyName("message")]
    public ChatMessage? Message { get; init; }

    [JsonPropertyName("finish_reason")]
    public string? FinishReason { get; init; }
}

/// <summary>Token usage reported for the request.</summary>
public sealed record ChatUsage
{
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; init; }

    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; init; }

    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; init; }
}
