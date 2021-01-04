using System.Threading;
using System.Threading.Tasks;
using AsyncSample.Dtos;
using AsyncSample.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsyncSample.Controllers
{
    [ApiController]
    [Route("api/weatherForecast")]
    public class WeatherForecastController : Controller
    {
        private readonly IWeatherForcecastService _weatherForcecastService;

        public WeatherForecastController(IWeatherForcecastService weatherForcecastService)
        {
            _weatherForcecastService = weatherForcecastService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(WeatherForecastRequestDto weatherForecastRequest, CancellationToken cancellationToken)
        {
            var result = await _weatherForcecastService.GetWeatherDataForCities(weatherForecastRequest.Cities, cancellationToken);

            return Ok(result);
        }
    }
}
