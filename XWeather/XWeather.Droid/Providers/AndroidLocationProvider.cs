using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using XWeather.Providers;

namespace XWeather.Droid.Providers
{
    // Heavily based on https://github.com/jamesmontemagno/GeolocatorPlugin/blob/master/src/Geolocator.Plugin.Android/GeolocatorImplementation.cs
    public class AndroidLocationProvider : ILocationProvider
    {
        private readonly LocationManager _manager;
        private readonly object _locationSync = new object();
        private readonly string[] _providers;

        private GeolocationSingleListener _listener;
        private Location _lastLocation;


        public AndroidLocationProvider()
        {
            _manager = (LocationManager) Application.Context.GetSystemService(Context.LocationService);
            _providers =
                _manager.GetProviders(enabledOnly: true).Where(p => p != LocationManager.PassiveProvider).ToArray();
        }


        public bool IsListening => _listener != null;

        public event EventHandler<PositionEventArgs> PositionChanged;


        public async Task<GeoLocation> GetPositionAsync()
        {
            var tcs = new TaskCompletionSource<GeoLocation>();

            // If I don't have the listener up, build it up, request a single location update and return the listener's inner task
            if (!IsListening)
            {
                _listener = new GeolocationSingleListener(500.0f, _providers.Where(_manager.IsProviderEnabled), () =>
                {
                    for (int i = 0; i < _providers.Length; i++)
                    {
                        _manager.RemoveUpdates(_listener);
                    }
                });

                try
                {
                    var enabled = 0;
                    foreach (var provider in _providers)
                    {
                        if (_manager.IsProviderEnabled(provider))
                            enabled++;

                        _manager.RequestSingleUpdate(provider, _listener, Looper.MainLooper);
                    }

                    if (enabled == 0)
                    {
                        foreach (var provider in _providers)
                        {
                            _manager.RemoveUpdates(_listener);
                        }

                        tcs.TrySetException(new InvalidOperationException());
                        return await tcs.Task.ConfigureAwait(false);
                    }
                }
                catch (Java.Lang.SecurityException securityException)
                {
                    tcs.TrySetException(new InvalidOperationException());
                    return await tcs.Task.ConfigureAwait(false);
                }

                return await _listener.Task.ConfigureAwait(false);
            }

            // If I had the listener up and running, simply use that listener or a last known location
            lock (_locationSync)
            {
                if (_lastLocation == null)
                {
                    EventHandler<PositionEventArgs> gotPosition = null;
                    gotPosition = (s, e) =>
                    {
                        tcs.TrySetResult(e.Location);
                        PositionChanged -= gotPosition;
                    };

                    PositionChanged += gotPosition;
                }
                else
                {
                    var geolocation = new GeoLocation()
                    {
                        Latitude = _lastLocation.Latitude,
                        Longitude = _lastLocation.Longitude
                    };
                    tcs.SetResult(geolocation);
                }
            }

            return await tcs.Task.ConfigureAwait(false);
        }

        public Task<bool> StartListeningAsync(int minTime, double minDistance)
        {
            throw new NotImplementedException();
        }

        public Task<bool> StopListeningAsync()
        {
            throw new NotImplementedException();
        }
    }
}