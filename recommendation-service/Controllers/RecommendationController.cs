using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace recommendation_service.Controllers;

[ApiController]
[Route("[controller]")]
public class RecommendationController(IRecommendationService recommendations) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Get([FromBody]RecommendationRequest request)
    {
        var response = await recommendations.Get(request);
        return Ok(response);
    }

    [HttpPut("template")]
    public async Task<IActionResult> Save([FromBody]UpdateTemplateRequest request)
    {
        await recommendations.Save(request);
        return Ok();
    }
}