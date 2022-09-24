using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.Benchmark.BusinessTest
{
    public class TestBenchmark
    {
        public static  IServiceProvider ServiceProvider { get; set; }
  
        [Benchmark]
        public void GetValue()
        {
            using var scope = ServiceProvider.CreateAsyncScope();
            var testBus = scope.ServiceProvider.GetService<Detachable.Project.IBusiness.ITest>();
            var result = testBus.GetValue();
        }
         
    }
}
