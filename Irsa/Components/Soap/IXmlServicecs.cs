using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Components.Soap
{
    public interface IXmlService
    {
        Task<string> InvokeService(string url, string parameter, string SoapAction);
    }
}
