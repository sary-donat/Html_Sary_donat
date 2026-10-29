
using Html_Sary_donat;
using System.Text.RegularExpressions;

using Html_Sary_donat;

var serializer = new HtmlSerializer();
var html = await serializer.Load("https://malkabruk.co.il/practicode");
var root = serializer.Serialize(html);

var selector = Selector.Parse("div p");
var results = root.FindElements(selector);

Console.WriteLine($"Found {results.Count()} elements");
foreach (var el in results)
    Console.WriteLine($"<{el.Name}> id={el.Id} classes={string.Join(",", el.Classes)}");
