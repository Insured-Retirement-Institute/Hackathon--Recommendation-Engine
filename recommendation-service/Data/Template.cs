namespace Data;

public class Template(string template)
{
    public string Build(dynamic? replacements = null)
    {
        if (replacements == null)
            return template;

        //original message should remain immutable
        var message = template;

        foreach (var replacement in replacements)
        {
            var keySearch = $"{{{replacement.Key}}}";
            message = message.Replace(keySearch, replacement.Value.ToString());
        }

        return message;
    }
}