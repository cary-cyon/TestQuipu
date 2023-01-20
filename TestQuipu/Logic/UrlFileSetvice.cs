using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestQuipu.Interfaces;
using TestQuipu.Model;

namespace TestQuipu.Logic
{
    internal class UrlFileSetvice : IFileService<UrlInfo>
    {
        public List<UrlInfo> Open(string filename)
        {
            var res = new List<UrlInfo>();
            foreach (string line in System.IO.File.ReadLines(filename))
            {
                res.Add(new UrlInfo(line, ""));
            }
            return res;
        }
    }
}
