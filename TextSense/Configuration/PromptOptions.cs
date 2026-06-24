namespace TextSense.Configuration;

public sealed class PromptOptions
{
    public const string SectionName = "Prompts";

    public string[] Samples { get; set; } = [];
}
