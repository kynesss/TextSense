namespace TextSense.Services;

public interface ITextAnalysisService
{
    Task<ServiceResult<TextAnalysis>> AnalyzeAsync(
        string text,
        string? language = null,
        CancellationToken cancellationToken = default);
}
