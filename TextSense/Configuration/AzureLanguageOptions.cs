namespace TextSense.Configuration;

/// <summary>
/// Strongly-typed settings for the Azure AI Language "analyze-text" API.
/// Bound from the "AzureLanguage" configuration section.
/// </summary>
public sealed class AzureLanguageOptions
{
    public const string SectionName = "AzureLanguage";

    /// <summary>Subscription key sent via the Ocp-Apim-Subscription-Key header.</summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>Base endpoint of the Language resource (e.g. https://&lt;resource&gt;.cognitiveservices.azure.com/).</summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>API version passed to the analyze-text endpoint.</summary>
    public string ApiVersion { get; set; } = "2024-11-01";

    /// <summary>Default language of the analyzed documents.</summary>
    public string DefaultLanguage { get; set; } = "en";
}
