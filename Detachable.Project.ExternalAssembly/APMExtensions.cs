using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly
{
    public class ApmExtensions
    {
        public IServiceCollection Services { get; }
        public ApmExtensions(IServiceCollection services)
        {
            Services = services;
        }
    }
}
