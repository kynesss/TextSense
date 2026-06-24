namespace TextSense.Services;

public interface ITextGenerationService
{
    Task<ServiceResult<string>> GenerateAsync(
        string prompt,
        string? model = null,
        CancellationToken cancellationToken = default);
}
