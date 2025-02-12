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
}
