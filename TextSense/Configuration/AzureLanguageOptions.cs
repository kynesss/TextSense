namespace TextSense.Configuration;

public sealed class AzureLanguageOptions
{
    public const string SectionName = "AzureLanguage";

    public string ApiKey { get; set; } = string.Empty;

    public string Endpoint { get; set; } = string.Empty;

    public string ApiVersion { get; set; } = "2024-11-01";

    public string DefaultLanguage { get; set; } = "en";
}
