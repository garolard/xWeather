using System.Net.Http;
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
            _client = new HttpClient(new NativeMessageHandler());
        }

        public static HttpProxy Instance => _instance ?? (_instance = new HttpProxy());

        public async Task<string> Get(string targetUri)
        {
            var response = await _client.GetAsync(targetUri);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Post(string targetUri, HttpContent postContent)
        {
            var response = await _client.PostAsync(targetUri, postContent);
            return await response.Content.ReadAsStringAsync();
        }
    }
}