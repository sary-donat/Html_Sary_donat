using System;
using System.Collections.Generic;
using System.Text;

namespace Html_Sary_donat
{
    internal static class HtmlElementExtensions
    {
        public static IEnumerable<HtmlElement> FindElements(this HtmlElement element, Selector selector)
        {
            var result = new HashSet<HtmlElement>();
            FindElementsRecursive(element, selector, result);
            return result;
        }

        private static void FindElementsRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> result)
        {
            var matches = element.Descendants().Skip(1).Where(e => IsMatch(e, selector));

            if (selector.Children == null)
            {
                foreach (var match in matches)
                    result.Add(match);
                return;
            }

            foreach (var match in matches)
                FindElementsRecursive(match, selector.Children, result);
        }

        private static bool IsMatch(HtmlElement element, Selector selector)
        {
            if (selector.TagName != null && element.Name != selector.TagName)
                return false;

            if (selector.Id != null && element.Id != selector.Id)
                return false;

            if (selector.Classes.Any(c => !element.Classes.Contains(c)))
                return false;

            return true;
        }
    }
}