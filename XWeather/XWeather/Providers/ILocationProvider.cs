using System;
using System.Threading;
using System.Threading.Tasks;

namespace XWeather.Providers
{
    public interface ILocationProvider
    {
        bool IsListening { get; }

        event EventHandler<PositionEventArgs> PositionChanged;

        Task<GeoLocation> GetPositionAsync();

        Task<bool> StartListeningAsync(int minTime, double minDistance);

        Task<bool> StopListeningAsync();
    }

    public class PositionEventArgs : EventArgs
    {
        public GeoLocation Location { get; private set; }

        public PositionEventArgs(GeoLocation location)
        {
            Location = location;
        }
    }

    public class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}