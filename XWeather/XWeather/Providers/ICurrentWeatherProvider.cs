using System.Threading;
using System.Threading.Tasks;
using XWeather.Dto;

namespace XWeather.Providers
{
    public interface ICurrentWeatherProvider
    {
        Task<CurrentWeatherDto> FindForCityCodeAsync(string cityCode, string units, CancellationTokenSource cancellationTokenSource);

        Task<CurrentWeatherDto> FindForCityNameAsync(string cityName, string units,  CancellationTokenSource cancellationTokenSource);

        Task<CurrentWeatherDto> FindForCoordinatesAsync(double latitude, double longitude, string units,
            CancellationTokenSource cancellationTokenSource);
    }
}