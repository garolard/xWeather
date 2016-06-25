using System.Threading;
using System.Threading.Tasks;
using XWeather.Dto;

namespace XWeather.Providers
{
    public interface ICurrentWeatherProvider
    {
        Task<WeatherDto> FindForCityCode(string cityCode, CancellationTokenSource cts);
        Task<WeatherDto> FindForCityName(string cityName, CancellationTokenSource cts);
    }
}