namespace Data._Configuration;

public static class DataConfiguration
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITemplatesRepo, TemplatesRepo>();

        return services;
    }
}