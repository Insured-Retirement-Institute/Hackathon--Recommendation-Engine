using System.Security.Cryptography.X509Certificates;

namespace Data;

public interface ITemplatesRepo
{
    Task SaveTemplate(string name, string template);
    Task<Template?> GetTemplate(string name);
}

public class TemplatesRepo : ITemplatesRepo
{
    //May replace with database
    private readonly string _basePath;

    public TemplatesRepo()
    {
        var slash = Path.DirectorySeparatorChar;
        _basePath = $"Data{slash}Templates{slash}";
    }

    public async Task SaveTemplate(string name, string template)
    {
        var fileRelativePath = $"{_basePath}{name}.txt";
        var fullPath = Path.Combine(AppContext.BaseDirectory, fileRelativePath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        await File.WriteAllTextAsync(fullPath, template);
    }

    public async Task<Template?> GetTemplate(string name)
    {
        var fileRelativePath = $"{_basePath}{name}.txt";
        var fullPath = Path.Combine(AppContext.BaseDirectory, fileRelativePath);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException(fullPath);

        var content = await File.ReadAllTextAsync(fullPath)
            ?? throw new Exception($"Could not load file {fullPath}");

        var template = new Template(content);
        return template;
    }
}
