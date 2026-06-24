namespace TextSense.Configuration;

/// <summary>
/// Strongly-typed settings for the OpenAI chat completions API.
/// Bound from the "OpenAI" configuration section.
/// </summary>
public sealed class OpenAiOptions
{
    public const string SectionName = "OpenAI";

    /// <summary>API key used as the Bearer token. Provided via user secrets or environment.</summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>Full chat completions endpoint URL.</summary>
    public string Endpoint { get; set; } = "https://api.openai.com/v1/chat/completions";

    /// <summary>Default model selected in the UI.</summary>
    public string DefaultModel { get; set; } = "gpt-4o-mini";

    /// <summary>Models offered to the user in the model picker.</summary>
    public string[] AvailableModels { get; set; } =
    [
        "gpt-4o-mini",
        "gpt-4o",
        "gpt-4.1-mini",
        "gpt-4.1"
    ];

    /// <summary>Upper bound on tokens generated per request.</summary>
    public int MaxTokens { get; set; } = 500;
}
