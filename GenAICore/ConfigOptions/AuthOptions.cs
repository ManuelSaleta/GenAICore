namespace GenAICore.ConfigOptions;

public class AuthOptions
{
    // Auth key for authorizing with the ChatGpt client
    // ReSharper disable once InconsistentNaming - the canonical correct convention is truly OpenAIApiKey
    public string OpenAIApiKey { get; set; } = string.Empty;
}