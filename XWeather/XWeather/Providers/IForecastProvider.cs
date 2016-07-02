using System.Threading;
using System.Threading.Tasks;
using XWeather.Dto;

namespace XWeather.Providers
{
    public interface IForecastProvider
    {
        Task<ForecastDto> FindForCityCodeAsync(string cityCode, string units, CancellationTokenSource cts);

        Task<ForecastDto> FindForCityNameAsync(string cityName, string units, CancellationTokenSource cts);

        Task<ForecastDto> FindForCoordinatesAsync(double latitude, double longitude, string units,
            CancellationTokenSource cancellationTokenSource);
    }
}