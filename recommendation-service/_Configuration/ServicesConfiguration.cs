using Client._Configuration;
using Data._Configuration;
using recommendation_service.Services;
using Services;

namespace _Configuration;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddTransient<IConversationService, ConversationService>()
            .AddTransient<IRecommendationService, RecommendationService>();

        services
            .AddConverationClient(config)
            .AddData(config);

        return services;
    }
}