using Amazon.BedrockRuntime;

namespace Client._Configuration;

public static class CovnersationClientConfiguration
{
    public static IServiceCollection AddConverationClient(this IServiceCollection services, IConfiguration config)
    {
        const string optionsSection = "Conversation";
        services.AddSingleton(config
            .GetSection(optionsSection)
            .Get<ConversationOptions>()
            ?? throw new Exception("Conversation configuration is invalid"));

        services.AddTransient<IConversationClient, ConversationClient>();
        services.AddAWSService<IAmazonBedrockRuntime>();

        return services;
    }
}
