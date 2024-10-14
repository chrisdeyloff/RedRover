using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RedRover.Parse;

public static class Parser
{
    private const string subPattern = @"\((?>\((?<DEPTH>)|\)(?<-DEPTH>)|[^()]+)*\)(?(DEPTH)(?!))";
    private const string pattern = @$"(?>\w+\.)?\w+{subPattern}|\w+";

    public static string ParseToMultilineNested(string input, bool sort = false)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;

        var list = ParseToList(input);
        var result = ParseToMultilineNested(list, sort);
        return result;
    }

    private static List<Property> ParseToList(string input)
    {
        List<Property> result = new();

        var matches = Regex.Matches(input, pattern);

        result = matches
            .Select(m => {
                var property = new Property();
                if (!Regex.IsMatch(m.Value, subPattern))
                { property.Name = m.Value; }
                else
                {
                    var firstParenIndex = m.Value.IndexOf('(');
                    property.Name = m.Value.Substring(0, firstParenIndex);
                    property.Properties = ParseToList(m.Value.Substring(firstParenIndex));
                }
                return property;
            })
            .ToList();

        return result;
    }

    private static string ParseToMultilineNested(List<Property> properties, bool sort, int indent = 0)
    {
        var result = new List<string>();
        if (sort) { properties = properties.OrderBy(p => p.Name).ToList(); }
        var padding = "".PadLeft(indent * 2, ' ');
        foreach (var property in properties)
        {
            result.Add($"{padding}- {property.Name}");
            if (property!.Properties?.Count > 0)
            { result.Add(ParseToMultilineNested(property.Properties, sort, ++indent)); }
        }

        return string.Join(Environment.NewLine, result);
    }
}

public class Property
{
    public string Name { get; set; }
    public List<Property> Properties { get; set; }
}