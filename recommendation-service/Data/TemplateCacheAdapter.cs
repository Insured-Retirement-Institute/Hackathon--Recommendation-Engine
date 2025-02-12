using Microsoft.Extensions.Caching.Memory;

namespace Data;

public class TemplateCacheAdapter(ITemplatesRepo repo, IMemoryCache cache) : ITemplatesRepo
{
    public async Task SaveTemplate(string name, string template)
    {
        await repo.SaveTemplate(name, template);
        cache.Remove(name);
    }

    public async Task<Template?> GetTemplate(string name)
    {
        var template = await cache.GetOrCreateAsync(name, async entry 
            => await repo.GetTemplate(name));

        return template;
    }
}