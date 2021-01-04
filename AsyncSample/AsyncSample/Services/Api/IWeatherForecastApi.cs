using Refit;
using System.Threading.Tasks;

namespace AsyncSample.Services.Api
{
    public interface IWeatherForecastApi
    {
        [Get("/weather?q={cityName}&appid={apiKey}")]
        Task<WeatherForecastApiResult> GetWeather(string cityName, string apiKey);
    }
}
