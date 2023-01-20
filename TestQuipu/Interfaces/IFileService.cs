using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestQuipu.Interfaces
{
    internal interface IFileService<T>
    {
        List<T> Open(string filename);
    }
}
