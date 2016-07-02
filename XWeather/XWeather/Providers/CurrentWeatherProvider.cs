using System;
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
    public class CurrentWeatherProvider : ICurrentWeatherProvider
    {
        private const string Endpoint = "weather";

        public async Task<CurrentWeatherDto> FindForCityCodeAsync(string cityCode, string units = "", CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource))
        {
            if (cancellationTokenSource == null)
                cancellationTokenSource = new CancellationTokenSource();

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

            var weather = await HttpProxy.Instance.GetAsync(Endpoint + "?" + query, cancellationTokenSource.Token);

            var possibleErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(weather);
            if (possibleErrorResponse.cod == 404)
                return null;

            var weatherEntity = JsonConvert.DeserializeObject<CurrentWeather>(weather);

            return new CurrentWeatherDto(weatherEntity);
        }
        
        public async Task<CurrentWeatherDto> FindForCityNameAsync(string cityName, string units = "", CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource))
        {
            if (cancellationTokenSource == null)
                cancellationTokenSource = new CancellationTokenSource();

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


            var weather = await HttpProxy.Instance.GetAsync(Endpoint + "?" + query, cancellationTokenSource.Token);

            var possibleErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(weather);
            if (possibleErrorResponse.cod == 404)
                return null;

            var weatherEntity = JsonConvert.DeserializeObject<CurrentWeather>(weather);

            return new CurrentWeatherDto(weatherEntity);
        }

        public async Task<CurrentWeatherDto> FindForCoordinatesAsync(double latitude, double longitude, string units = "", CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource))
        {
            if (latitude == -1 && longitude == -1) return null;

            if (cancellationTokenSource == null)
                cancellationTokenSource = new CancellationTokenSource();

            string query;

            var result = new List<KeyValuePair<string, string>>();

            result.Add(new KeyValuePair<string, string>("lat", latitude.ToString()));
            result.Add(new KeyValuePair<string, string>("lon", longitude.ToString()));
            result.Add(new KeyValuePair<string, string>("lang", "es"));
            if (!string.IsNullOrEmpty(units))
                result.Add(new KeyValuePair<string, string>("units", units));
            result.Add(new KeyValuePair<string, string>("appid", ApiConstants.WeatherApiKey));

            using (var content = new FormUrlEncodedContent(result.ToArray()))
            {
                query = await content.ReadAsStringAsync();
            }

            var weather = await HttpProxy.Instance.GetAsync(Endpoint + "?" + query, cancellationTokenSource.Token);

            var possibleErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(weather);
            if (possibleErrorResponse.cod == 404)
                return null;

            var weatherEntity = JsonConvert.DeserializeObject<CurrentWeather>(weather);

            return new CurrentWeatherDto(weatherEntity);
        }
    }
}