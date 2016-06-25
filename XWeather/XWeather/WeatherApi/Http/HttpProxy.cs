using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ModernHttpClient;

namespace XWeather.WeatherApi.Http
{
    public class HttpProxy
    {
        private readonly HttpClient _client;
        private static HttpProxy _instance;

        private HttpProxy()
        {
            _client = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(ApiConstants.WeatherBaseUri + ApiConstants.WeatherApiVersion + "/")
            };
        }

        public static HttpProxy Instance => _instance ?? (_instance = new HttpProxy());

        public async Task<string> GetAsync(string targetUri, CancellationTokenSource cts)
        {
            var localCancellationTokenSource = cts ?? new CancellationTokenSource();
            var response = await _client.GetAsync(targetUri, localCancellationTokenSource.Token);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string targetUri, HttpContent postContent, CancellationTokenSource cts)
        {
            var localCancellationTokenSource = cts ?? new CancellationTokenSource();
            var response = await _client.PostAsync(targetUri, postContent, localCancellationTokenSource.Token);
            return await response.Content.ReadAsStringAsync();
        }
    }
}