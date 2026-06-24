using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using TextSense.Configuration;
using TextSense.Models.OpenAi;

namespace TextSense.Services;

public sealed class OpenAiTextGenerationService : ITextGenerationService
{
    public const string HttpClientName = "OpenAI";

    private const string SystemPrompt = "You are a helpful assistant.";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OpenAiOptions _options;
    private readonly ILogger<OpenAiTextGenerationService> _logger;

    public OpenAiTextGenerationService(
        IHttpClientFactory httpClientFactory,
        IOptions<OpenAiOptions> options,
        ILogger<OpenAiTextGenerationService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<ServiceResult<string>> GenerateAsync(
        string prompt,
        string? model = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(prompt))
            return ServiceResult<string>.Failure("Prompt must not be empty.");

        var request = new ChatCompletionRequest
        {
            Model = string.IsNullOrWhiteSpace(model) ? _options.DefaultModel : model,
            MaxTokens = _options.MaxTokens,
            Messages =
            [
                new ChatMessage("system", SystemPrompt),
                new ChatMessage("user", prompt)
            ]
        };

        try
        {
            var client = _httpClientFactory.CreateClient(HttpClientName);
            using var response = await client.PostAsJsonAsync(string.Empty, request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError(
                    "OpenAI request failed with {StatusCode}: {Error}",
                    (int)response.StatusCode, error);
                return ServiceResult<string>.Failure(
                    $"Text generation failed (HTTP {(int)response.StatusCode}).");
            }

            var completion = await response.Content
                .ReadFromJsonAsync<ChatCompletionResponse>(cancellationToken);
            var content = completion?.Choices?.FirstOrDefault()?.Message?.Content?.Trim();

            return string.IsNullOrWhiteSpace(content)
                ? ServiceResult<string>.Failure("OpenAI returned an empty response.")
                : ServiceResult<string>.Success(content);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while calling OpenAI.");
            return ServiceResult<string>.Failure("Unexpected error while generating text.");
        }
    }
}
