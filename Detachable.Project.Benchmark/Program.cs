using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Detachable.Project.Benchmark.BusinessTest;
using Detachable.Project.WebApi.Extensions;

namespace Detachable.Project.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddService();
            var host = builder.Build();
            TestBenchmark.ServiceProvider = host.Services;
            var TestSummary = BenchmarkRunner.Run<TestBenchmark>();
            Console.ReadKey();
        }
    }
}