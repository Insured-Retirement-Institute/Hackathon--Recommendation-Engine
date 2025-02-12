using Client;
using Data;
using Models;
using System.Text.Json;
using static Models.RecommendationResponse;

namespace Services;

public interface IRecommendationService
{
    Task<RecommendationResponse> Get(RecommendationRequest request);
    Task Save(UpdateTemplateRequest request);
}

public class RecommendationService(ITemplatesRepo templates, IConversationClient conversation) : IRecommendationService
{
    public async Task<RecommendationResponse> Get(RecommendationRequest request)
    {
        var system = await templates.GetTemplate("RecommendationRequirements")
            ?? throw new Exception("Could not load questionare template.");

        var requestJson = JsonSerializer.Serialize(request);
        var recommendationJson = await conversation.Chat(requestJson, system.Build());
        var recommendation = JsonSerializer.Deserialize<Recommendation>(recommendationJson)
            ?? new Recommendation();

        var allocations = recommendation?.PortfolioAllocation?.AllSectors
            .SelectMany(s => s.Holdings)
            .Select(s => new AllocationModel
            {
                AssetClass = s.Category,
                AssetId = s.Symbol,
                AssetDisplayName = s.Name,
                AllocationPercentage = s.Weight
            })
            .ToList();

        var response = new RecommendationResponse
        {
            Allocations = allocations ?? [],
            Reason = recommendation?.Reason
        };

        return response;
    }

    public async Task Save(UpdateTemplateRequest request)
    {
        await templates.SaveTemplate(request.Name, request.Template);
    }
}
