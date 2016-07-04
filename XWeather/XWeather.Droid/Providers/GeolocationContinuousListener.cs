using System;
using System.Collections.Generic;
using System.Threading;
using Android.Locations;
using Android.OS;
using XWeather.Providers;

namespace XWeather.Droid.Providers
{
    public class GeolocationContinuousListener : Java.Lang.Object, ILocationListener
    {
        private readonly LocationManager _manager;
        private readonly HashSet<string> _providers;

        private Location _lastLocation;


        public GeolocationContinuousListener(LocationManager manager, IEnumerable<string> providers)
        {
            _manager = manager;
            _providers = new HashSet<string>(providers);
        }


        public EventHandler<PositionEventArgs> PositionChanged;


        public void OnLocationChanged(Location location)
        {
            var previous = Interlocked.Exchange(ref _lastLocation, location);
            if (previous != null)
                previous.Dispose();

            var geolocation = new GeoLocation()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            PositionChanged?.Invoke(this, new PositionEventArgs(geolocation));
        }

        public void OnProviderDisabled(string provider)
        {
            if (provider == LocationManager.PassiveProvider)
                return;

            lock (_providers)
            {
                if (_providers.Remove(provider) && _providers.Count == 0)
                    throw new InvalidOperationException();
            }
        }

        public void OnProviderEnabled(string provider)
        {
            if (provider == LocationManager.PassiveProvider)
                return;

            lock (_providers)
                _providers.Add(provider);
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            switch (status)
            {
                case Availability.Available:
                    OnProviderEnabled(provider);
                    break;
                case Availability.OutOfService:
                    OnProviderDisabled(provider);
                    break;
            }
        }
    }
}