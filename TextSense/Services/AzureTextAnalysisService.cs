using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using TextSense.Configuration;
using TextSense.Models.AzureLanguage;

namespace TextSense.Services;

public sealed class AzureTextAnalysisService : ITextAnalysisService
{
    public const string HttpClientName = "AzureLanguage";

    private const string SentimentKind = "SentimentAnalysis";
    private const string KeyPhraseKind = "KeyPhraseExtraction";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AzureLanguageOptions _options;
    private readonly ILogger<AzureTextAnalysisService> _logger;

    public AzureTextAnalysisService(
        IHttpClientFactory httpClientFactory,
        IOptions<AzureLanguageOptions> options,
        ILogger<AzureTextAnalysisService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<ServiceResult<TextAnalysis>> AnalyzeAsync(
        string text,
        string? language = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(text))
            return ServiceResult<TextAnalysis>.Failure("Text to analyze must not be empty.");

        var resolvedLanguage = string.IsNullOrWhiteSpace(language)
            ? _options.DefaultLanguage
            : language;

        try
        {
            var client = _httpClientFactory.CreateClient(HttpClientName);

            var sentimentDoc = await CallAnalyzeTextAsync(client, SentimentKind, text, resolvedLanguage, cancellationToken);
            var keyPhraseDoc = await CallAnalyzeTextAsync(client, KeyPhraseKind, text, resolvedLanguage, cancellationToken);

            var scores = sentimentDoc?.ConfidenceScores is { } c
                ? new SentimentScores(c.Positive, c.Neutral, c.Negative)
                : null;

            var analysis = new TextAnalysis
            {
                Sentiment = sentimentDoc?.Sentiment ?? "unknown",
                Scores = scores,
                KeyPhrases = keyPhraseDoc?.KeyPhrases ?? []
            };

            return ServiceResult<TextAnalysis>.Success(analysis);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while calling Azure Language.");
            return ServiceResult<TextAnalysis>.Failure("Unexpected error while analyzing text.");
        }
    }

    private async Task<AnalyzeTextDocument?> CallAnalyzeTextAsync(
        HttpClient client,
        string kind,
        string text,
        string language,
        CancellationToken cancellationToken)
    {
        var request = new AnalyzeTextRequest
        {
            Kind = kind,
            AnalysisInput = new AnalysisInput
            {
                Documents =
                [
                    new AnalysisDocument { Id = "1", Language = language, Text = text }
                ]
            },
            Parameters = new Dictionary<string, object>
            {
                ["modelVersion"] = "latest",
                ["loggingOptOut"] = false
            }
        };

        var endpoint = $"language/:analyze-text?api-version={_options.ApiVersion}";
        using var response = await client.PostAsJsonAsync(endpoint, request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError(
                "Azure Language {Kind} request failed with {StatusCode}: {Error}",
                kind, (int)response.StatusCode, error);
            throw new HttpRequestException($"Azure Language request failed (HTTP {(int)response.StatusCode}).");
        }

        var result = await response.Content.ReadFromJsonAsync<AnalyzeTextResponse>(cancellationToken);
        return result?.Results?.Documents?.FirstOrDefault();
    }
}
