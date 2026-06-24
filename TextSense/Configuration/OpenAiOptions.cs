namespace TextSense.Configuration;

public sealed class OpenAiOptions
{
    public const string SectionName = "OpenAI";

    public string ApiKey { get; set; } = string.Empty;

    public string Endpoint { get; set; } = "https://api.openai.com/v1/chat/completions";

    public string DefaultModel { get; set; } = "gpt-4o-mini";

    public string[] AvailableModels { get; set; } =
    [
        "gpt-4o-mini",
        "gpt-4o",
        "gpt-4.1-mini",
        "gpt-4.1"
    ];

    public int MaxTokens { get; set; } = 500;
}
