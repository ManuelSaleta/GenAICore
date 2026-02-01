using GenAICore.ConfigOptions;
using GenAILib.Clients;
using GenAILib.Parsers;
using Microsoft.Extensions.Options;

namespace GenAICore.Services;

public interface IAgentService
{
    public Task<string> ChatWithAsync(string prompt);
}

public class AgentService : IAgentService
{
    private readonly ChatGptClient _client;
    public AgentService(IOptions<AuthOptions> authOptions)
    {
        var authKey = authOptions.Value.OpenAIApiKey ?? throw new Exception("OpenAIApiKey is required");
        _client  = new ChatGptClient(authKey) ??  throw new ArgumentNullException(nameof(_client));
    }
    private readonly PromptParser _promptParser = new(); // TODO: Make into a service. Injectable via DI
    public async Task<string> ChatWithAsync(string prompt)
    {
        var parsedPrompt = _promptParser.ParsePrompt(prompt);
        var response = await _client!.ChatWithAsync(parsedPrompt);
        
        return response;
    }
}