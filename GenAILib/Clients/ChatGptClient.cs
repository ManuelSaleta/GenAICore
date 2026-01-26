using OpenAI.Chat;
using System;

namespace GenAILib.Clients;

public interface IChatGptClient
{
    /**
     * Takes a prompt typeof string.
     * Returns a string answer.
     */
    public Task<string> ChatAsync(string prompt);
}

public class ChatGptClient() : IChatGptClient, IDisposable
{
    // public IChatGptClient Client { get; } = client;
    
    private static readonly string AuthKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") 
                                              ?? throw new NullReferenceException("OPENAI_API_KEY");

    private ChatClient? _client = new(model: "gpt-4o", apiKey: AuthKey);
    

    public async Task<string> ChatAsync(string prompt)
    {
        var  chat = await _client!.CompleteChatAsync(prompt);

        if (chat == null) return "No answer yet.";
        
        var answered = chat.Value.Content[0].Text;
        
        return answered ?? "Failed to parse answer.";
    }

    public void Dispose()
    {
        _client = null;
    }
}
