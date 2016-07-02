using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using XWeather.Providers;

namespace XWeather.Uwp.Providers
{
    public class UwpLocationProvider : ILocationProvider
    {
        public async Task<GeoLocation> GetCurrentLocationAsync()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    var locator = new Geolocator()
                    {
                        DesiredAccuracyInMeters = 500
                    };
                    var position = await locator.GetGeopositionAsync();
                    return new GeoLocation()
                    {
                        Latitude = position.Coordinate.Latitude,
                        Longitude = position.Coordinate.Longitude
                    };
                default:
                    return new GeoLocation();
            }
        }
    }
}