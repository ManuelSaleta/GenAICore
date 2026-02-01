using OpenAI.Chat;
using System;

namespace GenAILib.Clients;

public interface IChatGptClient
{
    /**
     * Takes a prompt typeof string.
     * Returns a string answer.
     */
    public Task<string> ChatWithAsync(string prompt);
}

public class ChatGptClient(string authKey) : IChatGptClient, IDisposable
{
    private ChatClient? _client = new(model: "gpt-4o", apiKey: authKey);
    

    public async Task<string> ChatWithAsync(string prompt)
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
