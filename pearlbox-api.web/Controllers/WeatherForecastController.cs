using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace pearlbox_api.web.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<string> Get()
    {
        throw new Exception("Test");
        // return Enumerable.Range(1, 5).Select(index => "a")
        // .ToArray();
    }
}
