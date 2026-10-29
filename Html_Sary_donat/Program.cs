
using Html_Sary_donat;
using System.Text.RegularExpressions;

async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
var html = await Load("https://malkabruk.co.il/practicode");
var cleanHtml = new Regex("\\s+").Replace(html, " ");
var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => !string.IsNullOrWhiteSpace(s));
var root = new HtmlElement { Name = "root" };
var current = root;
foreach (var line in htmlLines)
{
    var firstWord = line.Split(' ')[0];
    if (firstWord == "/html")
        break;
    if (firstWord.StartsWith("/"))
    {
        if (current.Parent != null)
            current = current.Parent;
        continue;
    }
    var tagName = firstWord.TrimEnd('/');
    if (HtmlHelper.Instance.HtmlTags.Contains(tagName))
    {
        var element = new HtmlElement
        {
            Name = tagName,
            Parent = current
        };
        var rest = line.Substring(firstWord.Length);
        var matches = Regex.Matches(rest, "([^\\s]*?)=\"(.*?)\"");
        foreach (Match match in matches)
        {
            var name = match.Groups[1].Value;
            var value = match.Groups[2].Value;

            element.Attributes.Add($"{name}=\"{value}\"");

            if (name == "id")
                element.Id = value;

            if (name == "class")
                element.Classes.AddRange(value.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        }
        current.Children.Add(element);
        bool isSelfClosing = line.EndsWith("/") || HtmlHelper.Instance.HtmlVoidTags.Contains(tagName);
        if (!isSelfClosing)
            current = element;
        
    }
    else
    {
        current.InnerHtml += line;
    }
}
var selector = Selector.Parse("div p");
var results = root.FindElements(selector);
Console.WriteLine($"Found {results.Count()} elements");
foreach (var el in results)
    Console.WriteLine($"<{el.Name}> id={el.Id} classes={string.Join(",", el.Classes)}");