using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Html_Sary_donat
{
    internal class Selector
    {
        public string Id { get; set; }
        public string TagName { get; set; }
        public List<string> Classes { get; set; } = new List<string>();

        public Selector Parent { get; set; }
        public Selector Children { get; set; }
        public static Selector Parse(string query)
        {
            var levels = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Selector root = null;
            Selector current = null;

            foreach (var level in levels)
            {
                var selector = new Selector();
                var parts = Regex.Split(level, "(?=[#.])").Where(p => p != "");
                foreach (var part in parts)
                {
                    if (part.StartsWith("#"))
                        selector.Id = part.Substring(1);
                    else if (part.StartsWith("."))
                        selector.Classes.Add(part.Substring(1));
                    else if (HtmlHelper.Instance.HtmlTags.Contains(part))
                        selector.TagName = part;
                }
                if (root == null)
                {
                    root = selector;
                }
                else
                {
                    selector.Parent = current;
                    current.Children = selector;
                }

                current = selector;
            }

            return root;
        }
    }
}


