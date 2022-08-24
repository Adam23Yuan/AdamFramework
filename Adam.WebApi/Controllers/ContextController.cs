using Adam.WebApi.Context;
using Microsoft.AspNetCore.Mvc;

namespace Adam.WebApi.Controllers
{
    /// <summary>
    /// V1版本API
    /// </summary>
    [ApiController]
    [Route("api/[controller]/")]
    public class ContextController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ApplicationDbContext _context;
        private readonly ILogger<devV1Controller> _logger;

        public ContextController(ILogger<devV1Controller> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _context = applicationDbContext;
        }

        /// <summary>
        /// 获取数据 我来改点内容 试试 git add . 命令
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

        /// <summary>
        /// 获取数据
        /// <para> 参数 <paramref name="typeID"/> 数据类型为int </para>
        /// </summary>
        /// <param name="typeID">类型ID</param>
        /// <returns>返回天气集合</returns>
        [HttpGet]
        [Route("GetRoute2")]
        public IEnumerable<WeatherForecast> Get2(int typeID)
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)] + $"|typeID=[{typeID}]"
            })
            .ToArray();
        }
    }
}
