using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Html_Sary_donat
{
    internal class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }=new List<string>();
        public List<string> Classes { get; set; } = new List<string>();
        public string InnerHtml { get; set; }

        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();
        public IEnumerable<HtmlElement> Descendants()
        {
            var queue = new Queue<HtmlElement>();queue.Enqueue(this);
            while(queue.Count > 0)
            {
                var element = queue.Dequeue();
                yield return element;
                foreach (var child in element.Children)
                    queue.Enqueue(child);
            }
           
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            var current = this.Parent;

            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }
        public  IEnumerable<HtmlElement> FindElements( HtmlElement element, Selector selector)
        {
            var result = new HashSet<HtmlElement>();
            FindElementsRecursive(element, selector, result);
            return result;
        }
        private void FindElementsRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> result)
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
