using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Components.Soap
{
    public interface IXmlService
    {
        Task<string> PostXml(string url, string parameter);
    }
}
