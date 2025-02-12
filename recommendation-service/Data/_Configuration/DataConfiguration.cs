using Microsoft.Extensions.Caching.Memory;

namespace Data._Configuration;

public static class DataConfiguration
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration config)
    {
        services.AddMemoryCache();

        services.AddScoped<TemplatesRepo>();
        services.AddScoped<ITemplatesRepo>(provider =>
        {
            var repo = provider.GetRequiredService<TemplatesRepo>();
            var cache = provider.GetRequiredService<IMemoryCache>();
            var adapter = new TemplateCacheAdapter(repo, cache);
            return adapter;
        });

        return services;
    }
}