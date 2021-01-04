using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncSample.Services.Api
{
    public class WeatherForecastApiResult
    {
        [JsonProperty("main")]
        public WeatherMainData MainWeatherData { get; set; }

        [JsonProperty("name")]
        public string City { get; set; }
    }

    public class WeatherMainData
    {
        [JsonProperty("temp")]
        public double AverageTemperature { get; set; }

        [JsonProperty("temp_max")]
        public double MaximumTemperature { get; set; }

        [JsonProperty("temp_min")]
        public double MinimumTemperature { get; set; }
    }
}
