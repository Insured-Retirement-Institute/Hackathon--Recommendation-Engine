using Client._Configuration;
using recommendation_service.Services;

namespace _Configuration;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IConversationService, ConversationService>();
        services.AddConverationClient(config);

        return services;
    }
}