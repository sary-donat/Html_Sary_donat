using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Html_Sary_donat
{
   
    internal class HtmlHelper
    {
        private static readonly HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;


        public string[] HtmlTags { get; set; }
        public string[] HtmlVoidTags { get; set; }
        private HtmlHelper()
        {
          HtmlTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("HtmlTags.json"));
          HtmlVoidTags= JsonSerializer.Deserialize<string[]>(File.ReadAllText("HtmlVoidTags.json"));


        }
    }
}
