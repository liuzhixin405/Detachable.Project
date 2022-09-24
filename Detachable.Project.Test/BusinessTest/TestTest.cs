using Detachable.Project.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework.Interfaces;

namespace Detachable.Project.Test.ControllerTest
{
    internal class TestTest
    {
        IServiceProvider Services { set; get; }
        [SetUp]
        public void Setup()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddService();
            var host = builder.Build();
            this.Services = host.Services;
        }

        [Test]
        public void GetValueTest()
        {
            using var scope = Services.CreateAsyncScope();
            var testBus = scope.ServiceProvider.GetService<Detachable.Project.IBusiness.ITest>();
            var result = testBus.GetValue();
            Assert.AreEqual(result, "Success");
        }
    }
}
