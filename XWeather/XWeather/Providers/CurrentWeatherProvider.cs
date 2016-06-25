using System;
using System.Threading;
using System.Threading.Tasks;
using XWeather.Dto;

namespace XWeather.Providers
{
    public class CurrentWeatherProvider : ICurrentWeatherProvider
    {
        public async Task<WeatherDto> FindForCityCode(string cityCode, CancellationTokenSource cts)
        {
            return null;
        }

        public async Task<WeatherDto> FindForCityName(string cityName, CancellationTokenSource cts)
        {
            return null;
        }
    }
}