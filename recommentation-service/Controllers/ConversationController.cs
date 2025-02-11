using Microsoft.AspNetCore.Mvc;
using recommendation_service.Models;
using recommendation_service.Services;

namespace recommendation_service.Controllers;

[ApiController]
[Route("[controller]")]
public class ConversationController(IConversationService conversation) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Chat([FromBody]ConversationRequest request)
    {
        var response = await conversation.Chat(request);
        return Ok(response);
    }
}
