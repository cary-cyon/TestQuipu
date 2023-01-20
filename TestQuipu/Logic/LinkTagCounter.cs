using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestQuipu.Logic
{
    public class LinkTagCounter
    {
        public int CountLinkTags(string html)
        {
            html = html.Replace(" ", "");
            return Regex.Matches(html, "<a").Count;
        }
    }
}
