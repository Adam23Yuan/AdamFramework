using Adam.WebApi.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Adam.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //单例服务-不支持读取配置文件变更后的值
        private readonly PositionOptions _positionOptions;
        //scope服务-每次请求都是一个实例
        private readonly PositionOptions _snapshotOptionsSnapshot;
        //单例服务-支持读取配置文件变更后的值
        private readonly IOptionsMonitor<PositionOptions> _optionsDelegate;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<PositionOptions> positionOptions, IOptionsSnapshot<PositionOptions> snapshotOptionsAccessor, IOptionsMonitor<PositionOptions> optionsDelegate)
        {
            _logger = logger;
            _positionOptions = positionOptions.Value;
            _snapshotOptionsSnapshot = snapshotOptionsAccessor.Value;
            _optionsDelegate = optionsDelegate;
        }

        [HttpGet]
        [Route("GetRoute")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)] + $"-IOptions<PositionOptions>=>{_positionOptions.Name}|-IOptionsSnapshot<PositionOptions>=>{_snapshotOptionsSnapshot.Name}|-IOptionsMonitor<PositionOptions>=>{_optionsDelegate.CurrentValue.Name}"
            })
            .ToArray();
        }
    }
}
