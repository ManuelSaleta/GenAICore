using GenAICore.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenAICore.Controllers;

[Route("api/[controller]")]
[ApiController] 
public class AgentController(IAgentService agentService) : ControllerBase
{
    private readonly IAgentService _agentService = agentService 
                                                   ??  throw new ArgumentNullException(nameof(agentService));

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] string question)
    {
        var agentResponded = await _agentService.ChatWithAsync(question);
        
        return !string.IsNullOrEmpty(agentResponded) ? Ok(agentResponded) :  BadRequest();
    }
}