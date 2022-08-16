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
        private readonly TopItemSettings _monthTopItem;
        private readonly TopItemSettings _yearTopItem;
        private readonly IOptionsMonitor<TopItemSettings> _nameOptionsDelegate;

        IOptions<MyConfigOptions> _config;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IOptions<PositionOptions> positionOptions,
            IOptionsSnapshot<PositionOptions> snapshotOptionsAccessor,
            IOptionsMonitor<PositionOptions> optionsDelegate,
            IOptionsSnapshot<TopItemSettings> namedOptionsAccessor,
            IOptionsMonitor<TopItemSettings> nameOptionsDelegate,
            IOptions<MyConfigOptions> config)
        {
            _logger = logger;
            _positionOptions = positionOptions.Value;
            _snapshotOptionsSnapshot = snapshotOptionsAccessor.Value;
            _optionsDelegate = optionsDelegate;


            _nameOptionsDelegate = nameOptionsDelegate;

            _monthTopItem = namedOptionsAccessor.Get(TopItemSettings.Month);
            _yearTopItem = namedOptionsAccessor.Get(TopItemSettings.Year);
            _config = config;

            try
            {
                var configValue = _config.Value;
            }
            catch (OptionsValidationException ex)
            {
                foreach (var failure in ex.Failures)
                {
                    _logger.LogError(failure);
                }
            }
        }

        [HttpGet]
        [Route("GetRoute")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 1).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = $"-IOptions<PositionOptions>=>{_positionOptions.Name}|-IOptionsSnapshot<PositionOptions>=>{_snapshotOptionsSnapshot.Name}|-IOptionsMonitor<PositionOptions>=>{_optionsDelegate.CurrentValue.Name}",
                OptionText = $"Month {_monthTopItem.Name} : {_monthTopItem.Model} | Year {_yearTopItem.Name} : {_yearTopItem.Model}|||" +
                $"Month:{_nameOptionsDelegate.Get(TopItemSettings.Month).Name}|||" +
                $"OptionsValidation: {_config.Value.Key1}:{_config.Value.Key2}:{_config.Value.Key3}"
            })
            .ToArray();
        }
        [HttpPost]
        [Route("PostConfigureAll")]
        public bool PostConfigureAll()
        {
            return true;
        }
    }
}
