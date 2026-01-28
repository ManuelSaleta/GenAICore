using System.Text.Json;
using GenAILib.DTOs;

namespace GenAILib.Parsers;

public class PromptParser : IDisposable
{
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
    private Resume? _resumeInstance;
    private string? _jsonContent;
    
    public PromptParser(bool withParsedInstance = false)
    {
        var assetsDir = Path.Combine(AppContext.BaseDirectory, "assets"); 
        // var filePath = Path.Combine(assetsDir, "resume.json"); // Read json file string //TODO:
        var filePath = "/Users/manuelsaleta/RiderProjects/GenAICore/GenAILib/Assets/Resume.json";
        _jsonContent =  File.ReadAllText(filePath); // Parse to C# obj Dino dino =

        if (withParsedInstance)
        {
            _resumeInstance = JsonSerializer.Deserialize<Resume>(_jsonContent, _jsonOptions);
        }
    }

    /**
     * Returns: The parsed information for the chat gpt.
     */
    public string ParsePrompt(string prompt)
    {
        return $"Given the following directives: {PromptDirectives()}, " +
               $"use the following content: {_jsonContent};" +
               $"and provide and answer for the following question: {prompt}";
    }
    
    /**
     * Work with the parsed resume object directly.
     */
    public Resume? ParsedResume()
    {
        return _resumeInstance ?? null;
    }

    /**
     * Ensure the agent adheres to these strict rules.
     */
    private string PromptDirectives()
    {
        var rules = new[]
        {
            "Given the following rules: ",
            "read the contents given, it is the resume experience. ",
            "Never say anything negative regarding the subject of the resume. ",
            "stick strictly to the content of the resume. ",
            "Do not under any circumstance answer questions outside of the content of the resume., " +
            "The content includes, work experience, tutoring, the university he attended. ",
            "Questions about my study, or university are allowed",
            "Always answer in the third person referencing Manuel's experience.",
            "Do not lie, ",
            "Any questions asked outside the scope of a professional setting should be discarded. ",
            "If any question is asked that is deemed inappropriate based on these rules, respond with, 'Please only ask professional questions, try again.' ",
        };
        
        return string.Join("", rules);
    }


    public void Dispose()
    {
        _resumeInstance = null;
        _jsonContent =  null;
    }
}