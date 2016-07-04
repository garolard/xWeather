using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using XWeather.Providers;

namespace XWeather.Uwp.Providers
{
    public class UwpLocationProvider : ILocationProvider
    {
        private bool _isListening;
        private Geolocator _locator;


        public bool IsListening { get; }

        public event EventHandler<PositionEventArgs> PositionChanged;


        public async Task<GeoLocation> GetPositionAsync()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    _locator = new Geolocator();
                    var position = await _locator.GetGeopositionAsync();
                    return new GeoLocation()
                    {
                        Latitude = position.Coordinate.Latitude,
                        Longitude = position.Coordinate.Longitude
                    };
                default:
                    return null;
            }
        }

        public Task<bool> StartListeningAsync(int minTime, double minDistance)
        {
            if (minTime < 0)
                throw new ArgumentOutOfRangeException(nameof(minTime));
            if (minDistance < 0)
                throw new ArgumentOutOfRangeException(nameof(minDistance));
            if (IsListening)
                throw new InvalidOperationException();

            _isListening = true;

            var locator = _locator;
            locator.ReportInterval = (uint) minTime;
            locator.MovementThreshold = minDistance;
            locator.PositionChanged += OnPositionChanged;

            return Task.FromResult(true);
        }

        public Task<bool> StopListeningAsync()
        {
            if (!IsListening)
                return Task.FromResult(true);

            _locator.PositionChanged -= OnPositionChanged;
            _isListening = false;

            return Task.FromResult(true);
        }


        private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            var position = new GeoLocation()
            {
                Latitude = args.Position.Coordinate.Latitude,
                Longitude = args.Position.Coordinate.Longitude
            };
            PositionChanged?.Invoke(this, new PositionEventArgs(position));
        }
    }
}