using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XWeather.Dto;
using XWeather.Entities;
using XWeather.WeatherApi.Http;

namespace XWeather.Providers
{
    public class CurrentWeatherProvider : ICurrentWeatherProvider
    {
        public async Task<CurrentWeatherDto> FindForCityCode(string cityCode, CancellationTokenSource cts)
        {
            if (cts == null)
                cts = new CancellationTokenSource();

            const string endpoint = "weather";
            string query;
            using (var content = new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string>("id", cityCode),
                new KeyValuePair<string, string>("appid", ApiConstants.WeatherApiKey), 
            }))
            {
                query = await content.ReadAsStringAsync();
            }

            var weather = await HttpProxy.Instance.GetAsync(endpoint + "?" + query, cts.Token);
            var weatherEntity = JsonConvert.DeserializeObject<CurrentWeather>(weather);

            if (weatherEntity.cod == 404)
                return null;
            
            return new CurrentWeatherDto(weatherEntity);
        }

        public async Task<CurrentWeatherDto> FindForCityName(string cityName, CancellationTokenSource cts)
        {
            if (cts == null)
                cts = new CancellationTokenSource();

            const string endpoint = "weather";
            string query;
            using (var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("q", cityName),
                new KeyValuePair<string, string>("appid", ApiConstants.WeatherApiKey), 
            }))
            {
                query = await content.ReadAsStringAsync();
            }


            var weather = await HttpProxy.Instance.GetAsync(endpoint + "?" + query, cts.Token);
            var weatherEntity = JsonConvert.DeserializeObject<CurrentWeather>(weather);

            if (weatherEntity.cod == 404)
                return null;

            return new CurrentWeatherDto(weatherEntity);
        }
    }
}