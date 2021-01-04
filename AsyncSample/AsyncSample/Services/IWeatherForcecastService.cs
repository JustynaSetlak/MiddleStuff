using AsyncSample.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSample.Services
{
    public interface IWeatherForcecastService
    {
        Task<List<WeatherForecastResultDto>> GetWeatherDataForCities(List<string> cities, CancellationToken cancellationToken = default);
    }
}
