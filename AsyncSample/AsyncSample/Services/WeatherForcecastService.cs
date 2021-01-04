using AsyncSample.Dtos;
using AsyncSample.Options;
using AsyncSample.Services.Api;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSample.Services
{
    public class WeatherForcecastService : IWeatherForcecastService
    {
        private readonly ILogger<WeatherForcecastService> _logger;
        private readonly IWeatherForecastApi _weatherForecastApi;
        private readonly WeatherApiOptions _weatherApiOptions;

        public WeatherForcecastService(
            ILogger<WeatherForcecastService> logger, 
            IWeatherForecastApi weatherForecastApi,
            IOptions<WeatherApiOptions> options)
        {
            _logger = logger;
            _weatherForecastApi = weatherForecastApi;
            _weatherApiOptions = options.Value;
        }

        public async Task<List<WeatherForecastResultDto>> GetWeatherDataForCities(List<string> cities, CancellationToken cancellationToken = default)
        {
            //long running operation
            var operationDurationTime = 100;
            await Task.Delay(operationDurationTime);

            cancellationToken.ThrowIfCancellationRequested();

            var tasks = cities
                .Select(city => 
                { 
                    var task = GetAverageTemperatureData(city); 
                    task.ContinueWith(x => _logger.LogInformation($"Data received for city: {city}")); 
                    return task; 
                });

            var data = await Task.WhenAll(tasks);

            return data.ToList();
        }

        public async Task<WeatherForecastResultDto> GetAverageTemperatureData(string city)
        {
            var millisecondsTimeout = 100;

            using (var cts = new CancellationTokenSource(millisecondsTimeout))
            {
                try
                {
                    //long-running operation
                    var operationDuration = 120;
                    await Task.Delay(operationDuration, cts.Token);

                    var weatherData = await _weatherForecastApi.GetWeather(city, _weatherApiOptions.ApiKey);

                    return new WeatherForecastResultDto
                    {
                        City = weatherData.City,
                        Temperature = weatherData.MainWeatherData.AverageTemperature
                    };
                }
                catch (TaskCanceledException)
                {
                    this._logger.LogError($"Api call for city was longer than allowed");
                    
                    return new WeatherForecastResultDto
                    {
                        City = city,
                        Temperature = null
                    };
                }
            };
        }
    }
}
