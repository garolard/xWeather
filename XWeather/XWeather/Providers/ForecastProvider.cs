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
        private const string Endpoint = "forecast";

        public async Task<ForecastDto> FindForCityCodeAsync(string cityCode, string units = "", CancellationTokenSource cts = default(CancellationTokenSource))
        {
            if (cts == null)
                cts = new CancellationTokenSource();

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

            var forecast = await HttpProxy.Instance.GetAsync(Endpoint + "?" + query, cts.Token);

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

            var forecast = await HttpProxy.Instance.GetAsync(Endpoint + "?" + query, cts.Token);

            var possibleErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(forecast);
            if (possibleErrorResponse.cod == 404)
                return null;

            var forecastEntity = JsonConvert.DeserializeObject<Forecast>(forecast);

            return new ForecastDto(forecastEntity);
        }

        public async Task<ForecastDto> FindForCoordinatesAsync(double latitude, double longitude, string units = "",
            CancellationTokenSource cts = default(CancellationTokenSource))
        {
            if (latitude == -1 && longitude == -1) return null;

            if (cts == null)
                cts = new CancellationTokenSource();

            string query;

            var result = new List<KeyValuePair<string, string>>();
            
            result.Add(new KeyValuePair<string, string>("lang", "es"));
            if (!string.IsNullOrEmpty(units))
                    result.Add(new KeyValuePair<string, string>("units", units));
            result.Add(new KeyValuePair<string, string>("appid", ApiConstants.WeatherApiKey));

            using (var content = new FormUrlEncodedContent(result.ToArray()))
            {
                query = string.Format("lat={0}&lon={1}&", latitude.ToString().Replace(',', '.'), longitude.ToString().Replace(',', '.'));
                query += await content.ReadAsStringAsync();
            }

            var forecast = await HttpProxy.Instance.GetAsync(Endpoint + "?" + query, cts.Token);

            var possibleErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(forecast);
            if (possibleErrorResponse.cod == 404)
                return null;

            var forecastEntity = JsonConvert.DeserializeObject<Forecast>(forecast);

            return new ForecastDto(forecastEntity);
        }
    }
}