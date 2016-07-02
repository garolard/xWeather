using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using XWeather.Dto;
using XWeather.Messages;
using XWeather.Providers;

namespace XWeather.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly ICurrentWeatherProvider _weatherProvider;
        private readonly ILocationProvider _locationProvider;
        private readonly IMvxMessenger _messenger;

        private string _cityName;
        private CurrentWeatherDto _currentWeather;


        public MainViewModel()
        {
            _weatherProvider = Mvx.Resolve<ICurrentWeatherProvider>();
            _locationProvider = Mvx.Resolve<ILocationProvider>();
            _messenger = Mvx.Resolve<IMvxMessenger>();

            CurrentWeather = new CurrentWeatherDto();

            GetCurrentWeatherCommand = new MvxAsyncCommand(GetCurrentWeatherAsync);
            SendChangeBackgroundMessageCommand = new MvxCommand(SendChangeBackgroundMessage);
        }


        public string CityName
        {
            get { return _cityName; }
            set { _cityName = value; RaisePropertyChanged(); }
        }

        public CurrentWeatherDto CurrentWeather
        {
            get { return _currentWeather; }
            set { _currentWeather = value; RaisePropertyChanged(); }
        }

        public IMvxCommand GetCurrentWeatherCommand { get; }

        public IMvxCommand SendChangeBackgroundMessageCommand { get; }


        public void Init()
        {
            GetCurrentWeatherCommand.Execute();
        }


        private async Task GetCurrentWeatherAsync()
        {
            var currentLocation = await _locationProvider.GetCurrentLocationAsync();
            CurrentWeather =
                await _weatherProvider.FindForCoordinatesAsync(currentLocation.Latitude, currentLocation.Longitude, "metric", new CancellationTokenSource());
            if (CurrentWeather != null)
                SendChangeBackgroundMessageCommand.Execute();
        }

        private void SendChangeBackgroundMessage()
        {
            _messenger.Publish(new NeedChangeBackgroundColorMessage(this, CurrentWeather.Clouds.All));
        }
    }
}