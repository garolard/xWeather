using System.Threading;
using System.Threading.Tasks;

namespace XWeather.Providers
{
    public interface ILocationProvider
    {
        Task<GeoLocation> GetCurrentLocationAsync();
    }

    public class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}