using System;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using XWeather.Providers;

namespace XWeather.Droid.Providers
{
    public class AndroidLocationProvider : ILocationProvider, ILocationListener
    {
        private readonly Context _context;
        private string _locationProvider;
        private LocationManager _locationManager;
        private Location _location;


        public AndroidLocationProvider(Context context)
        {
            _context = context;
        }


        public Task<GeoLocation> GetCurrentLocationAsync()
        {
            InitLocationManager();
            var location = _locationManager.GetLastKnownLocation(_locationProvider);

            if (location != null)
                return Task.FromResult(new GeoLocation()
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                });
            else
                return null;
        }

        private void InitLocationManager()
        {
            _locationManager = (LocationManager) _context.GetSystemService(Context.LocationService);
            var criteriaForLocationService = new Criteria()
            {
                Accuracy = Accuracy.Medium
            };

            var acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService,
                enabledOnly: true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IntPtr Handle { get; }

        public void OnLocationChanged(Location location)
        {
            _location = location;
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}