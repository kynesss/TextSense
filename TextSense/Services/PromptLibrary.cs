using Microsoft.Extensions.Options;
using TextSense.Configuration;

namespace TextSense.Services;

public sealed class PromptLibrary : IPromptLibrary
{
    private readonly PromptOptions _options;

    public PromptLibrary(IOptions<PromptOptions> options)
    {
        _options = options.Value;
    }

    public IReadOnlyList<string> GetSamplePrompts() => _options.Samples;
}
