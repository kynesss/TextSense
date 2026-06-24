namespace TextSense.Services;

public interface IPromptLibrary
{
    IReadOnlyList<string> GetSamplePrompts();
}
