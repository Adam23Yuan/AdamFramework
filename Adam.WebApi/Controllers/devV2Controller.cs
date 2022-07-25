using Adam.WebApi.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Adam.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V2))]
    public class devV2Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<devV2Controller> _logger;

        public devV2Controller(ILogger<devV2Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetRoute")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}