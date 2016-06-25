using System.Threading;
using System.Threading.Tasks;
using XWeather.Dto;

namespace XWeather.Providers
{
    public interface ICurrentWeatherProvider
    {
        Task<CurrentWeatherDto> FindForCityCode(string cityCode, CancellationTokenSource cts);
        Task<CurrentWeatherDto> FindForCityName(string cityName, CancellationTokenSource cts);
    }
}