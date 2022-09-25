using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly.Diagnostics
{
    public interface IHostingDiagnosticHandler
    {
        bool OnlyMatch(HttpContext httpContext);

        void BeginRequest(HttpContext httpContext);

        void EndRequest(HttpContext httpContext);
    }
}
