using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly
{
    public class DefaultHostingDiagnosticHandler : IHostingDiagnosticHandler
    {

        public bool OnlyMatch(HttpContext request)
        {
            return true;
        }

        public void BeginRequest(HttpContext httpContext)
        {
            //捕获方法开始前的参数
            Console.WriteLine("request start...");
        }

        public void EndRequest(HttpContext httpContext)
        {
            //捕获方法结束前的参数
            Console.WriteLine("request end...");
        }
    }
}
