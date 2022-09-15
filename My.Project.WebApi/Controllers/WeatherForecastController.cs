using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using My.Project.Business;
using My.Project.IBusiness;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.Project.WebApi.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ITest _test;
    private readonly TestNoInterface _testNoInterface;
    public WeatherForecastController(ILogger<WeatherForecastController> logger,ITest test, TestNoInterface testNoInterface)
    {
        _logger = logger;
        _test = test;
        _testNoInterface = testNoInterface;
    }

    [HttpGet]
    public async Task<string> HelloWorld()
    {
       await RedisHelper.SetAsync("helloworld_key","helloworld");
        return _test.GetValue();
    }
    [HttpGet]
    public async Task<string> GetYeah()
    {
        return _testNoInterface.Get() + await RedisHelper.GetAsync("helloworld_key");
    }
}
