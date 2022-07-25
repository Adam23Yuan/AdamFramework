using Adam.WebApi.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Adam.WebApi.Controllers
{
    /// <summary>
    /// V1版本API
    /// </summary>
    [ApiController]
    [Route("api/[controller]/")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V1))]
    public class devV1Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<devV1Controller> _logger;

        public devV1Controller(ILogger<devV1Controller> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
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