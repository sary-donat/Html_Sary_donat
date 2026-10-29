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
        
    }
}
