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

        private GeolocationContinuousListener _listener;
        private Location _lastLocation;
        private string[] _providers;


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
                GeolocationSingleListener listener = null;
                listener = new GeolocationSingleListener(500.0f, _providers.Where(_manager.IsProviderEnabled), () =>
                {
                    for (int i = 0; i < _providers.Length; i++)
                    {
                        _manager.RemoveUpdates(listener);
                    }
                });

                try
                {
                    var enabled = 0;
                    foreach (var provider in _providers)
                    {
                        if (_manager.IsProviderEnabled(provider))
                            enabled++;

                        var looper = Looper.MyLooper() ?? Looper.MainLooper;
                        _manager.RequestSingleUpdate(provider, listener, looper);
                    }

                    if (enabled == 0)
                    {
                        foreach (var provider in _providers)
                        {
                            _manager.RemoveUpdates(listener);
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

                return await listener.Task.ConfigureAwait(false);
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
            if (minTime < 0)
                throw new ArgumentOutOfRangeException(nameof(minTime));
            if (minDistance < 0)
                throw new ArgumentOutOfRangeException(nameof(minDistance));

            if (_providers.Length == 0)
                _providers =
                    _manager.GetProviders(enabledOnly: true).Where(p => p != LocationManager.PassiveProvider).ToArray();

            _listener = new GeolocationContinuousListener(_manager, _providers);
            _listener.PositionChanged += OnPositionChanged;

            var looper = Looper.MyLooper() ?? Looper.MainLooper;
            foreach (var provider in _providers)
            {
                _manager.RequestLocationUpdates(provider, minTime, (float)minDistance, _listener, looper);
            }

            return Task.FromResult(true);
        }
        
        public Task<bool> StopListeningAsync()
        {
            if (_listener == null)
                return Task.FromResult(true);

            _listener.PositionChanged -= OnPositionChanged;

            foreach (var provider in _providers)
            {
                _manager.RemoveUpdates(_listener);
            }

            _listener = null;
            return Task.FromResult(true);
        }


        private void OnPositionChanged(object sender, PositionEventArgs positionEventArgs)
        {
            if (!IsListening)
                return;

            PositionChanged?.Invoke(this, positionEventArgs);
        }
    }
}