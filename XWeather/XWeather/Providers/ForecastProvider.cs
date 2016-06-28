using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XWeather.Dto;
using XWeather.Entities;
using XWeather.WeatherApi.Http;
using XWeather.WeatherApi.StatusResponses;

namespace XWeather.Providers
{
    public class ForecastProvider : IForecastProvider
    {
        public async Task<ForecastDto> FindForCityCodeAsync(string cityCode, string units = "", CancellationTokenSource cts = default(CancellationTokenSource))
        {
            if (cts == null)
                cts = new CancellationTokenSource();

            const string endpoint = "forecast";
            string query;

            var result = new List<KeyValuePair<string, string>>();

            result.Add(new KeyValuePair<string, string>("id", cityCode));

            if (!string.IsNullOrEmpty(units))
                result.Add(new KeyValuePair<string, string>("units", units));

            result.Add(new KeyValuePair<string, string>("appid", ApiConstants.WeatherApiKey));

            using (var content = new FormUrlEncodedContent(result.ToArray()))
            {
                query = await content.ReadAsStringAsync();
            }

            var forecast = await HttpProxy.Instance.GetAsync(endpoint + "?" + query, cts.Token);

            var possibleErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(forecast);
            if (possibleErrorResponse.cod == 404)
                return null;

            var forecastEntity = JsonConvert.DeserializeObject<Forecast>(forecast);

            return new ForecastDto(forecastEntity);
        }

        public async Task<ForecastDto> FindForCityNameAsync(string cityName, string units = "", CancellationTokenSource cts = default(CancellationTokenSource))
        {
            if (cts == null)
                cts = new CancellationTokenSource();

            const string endpoint = "forecast";
            string query;

            var result = new List<KeyValuePair<string, string>>();

            result.Add(new KeyValuePair<string, string>("q", cityName));

            if (!string.IsNullOrEmpty(units))
                result.Add(new KeyValuePair<string, string>("units", units));

            result.Add(new KeyValuePair<string, string>("appid", ApiConstants.WeatherApiKey));

            using (var content = new FormUrlEncodedContent(result.ToArray()))
            {
                query = await content.ReadAsStringAsync();
            }

            var forecast = await HttpProxy.Instance.GetAsync(endpoint + "?" + query, cts.Token);

            var possibleErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(forecast);
            if (possibleErrorResponse.cod == 404)
                return null;

            var forecastEntity = JsonConvert.DeserializeObject<Forecast>(forecast);

            return new ForecastDto(forecastEntity);
        }
    }
}