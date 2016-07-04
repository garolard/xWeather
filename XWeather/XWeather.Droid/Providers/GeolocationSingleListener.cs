using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Locations;
using Android.OS;
using XWeather.Providers;

namespace XWeather.Droid.Providers
{
    public class GeolocationSingleListener : Java.Lang.Object, ILocationListener
    {
        private readonly float _desiredAccuracy;
        private readonly object _locationSync = new object();
        private readonly TaskCompletionSource<GeoLocation> _completionSource = new TaskCompletionSource<GeoLocation>();
        private readonly HashSet<string> _activeProviders;
        private readonly Action _finishCallback;

        private Location _bestLocation;


        public GeolocationSingleListener(float desiredAccuracy, IEnumerable<string> activeProviders, Action finishCallback)
        {
            _desiredAccuracy = desiredAccuracy;
            _activeProviders = new HashSet<string>(activeProviders);
            _finishCallback = finishCallback;
        }


        public Task<GeoLocation> Task => _completionSource.Task;


        public void OnLocationChanged(Location location)
        {
            if (location.Accuracy < _desiredAccuracy)
            {
                Finish(location);
                return;
            }

            lock (_locationSync)
            {
                if (_bestLocation == null || location.Accuracy < _bestLocation.Accuracy)
                    _bestLocation = location;
            }
        }
        
        public void OnProviderDisabled(string provider)
        {
            lock (_activeProviders)
            {
                if (_activeProviders.Remove(provider) && _activeProviders.Count == 0)
                    _completionSource.TrySetException(new InvalidOperationException());
            }
        }

        public void OnProviderEnabled(string provider)
        {
            lock (_activeProviders)
                _activeProviders.Add(provider);
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

        public void Cancel()
        {
            _completionSource.TrySetCanceled();
        }


        private void Finish(Location location)
        {
            var geolocation = new GeoLocation()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            _finishCallback?.Invoke();

            _completionSource.TrySetResult(geolocation);
        }
    }
}