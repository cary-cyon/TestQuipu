using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestQuipu.Model
{
    internal class UrlInfo
    {
        public string Url { get; set; }
        public string ColOfLinkTags { get; set; }
        public bool MaxValue { get; set; }
        public UrlInfo(string url, string col)
        {
            Url = url;
            ColOfLinkTags = col;
            MaxValue = false;
        }
        public override string ToString()
        {
            return Url;
        }
    }
}
