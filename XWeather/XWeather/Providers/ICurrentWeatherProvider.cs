using System.Threading;
using System.Threading.Tasks;
using XWeather.Dto;

namespace XWeather.Providers
{
    public interface ICurrentWeatherProvider
    {
        Task<CurrentWeatherDto> FindForCityCodeAsync(string cityCode, string units, CancellationTokenSource cts);
        Task<CurrentWeatherDto> FindForCityNameAsync(string cityName, string units,  CancellationTokenSource cts);
    }
}