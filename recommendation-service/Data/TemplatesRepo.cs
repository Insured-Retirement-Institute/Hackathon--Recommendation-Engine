namespace Data;

public interface ITemplatesRepo
{
    Task<Template?> GetTemplate(string name);
}

public class TemplatesRepo : ITemplatesRepo
{
    //May replace with database

    public async Task<Template?> GetTemplate(string name)
    {
        var fileRelativePath = $"Data\\Templates\\{name}.txt";
        var fullPath = Path.Combine(AppContext.BaseDirectory, fileRelativePath);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException(fullPath);

        var content = await File.ReadAllTextAsync(fullPath)
            ?? throw new Exception($"Could not load file {fullPath}");

        var template = new Template(content);
        return template;
    }
}
