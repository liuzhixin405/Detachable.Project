using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using My.Project.Business;
using My.Project.IBusiness;
using System.Collections.Generic;
using System.Linq;

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
    public string HelloWorld()
    {
        return _test.GetValue();
    }
    [HttpGet]
    public string GetYeah()
    {
        return _testNoInterface.Get();
    }
}
