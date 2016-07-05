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
        private readonly IForecastProvider _forecastProvider;
        private readonly ILocationProvider _locationProvider;
        private readonly IMvxMessenger _messenger;

        private bool _isBusy;
        private string _cityName;
        private CurrentWeatherDto _currentWeather;
        private ForecastDto _forecast;


        public MainViewModel()
        {
            _weatherProvider = Mvx.Resolve<ICurrentWeatherProvider>();
            _forecastProvider = Mvx.Resolve<IForecastProvider>();
            _locationProvider = Mvx.Resolve<ILocationProvider>();
            _messenger = Mvx.Resolve<IMvxMessenger>();

            CurrentWeather = new CurrentWeatherDto();
            Forecast = new ForecastDto();

            GetCurrentWeatherCommand = new MvxAsyncCommand(GetCurrentWeatherAsync);
            SendChangeBackgroundMessageCommand = new MvxCommand(SendChangeBackgroundMessage);
        }


        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(); }
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

        public ForecastDto Forecast
        {
            get { return _forecast; }
            set { _forecast = value; RaisePropertyChanged(); }
        }

        public IMvxCommand GetCurrentWeatherCommand { get; }

        public IMvxCommand SendChangeBackgroundMessageCommand { get; }


        public void Init()
        {
            GetCurrentWeatherCommand.Execute();

        }


        private async Task GetCurrentWeatherAsync()
        {
            IsBusy = true;

            var currentLocation = await _locationProvider.GetPositionAsync();
            CurrentWeather =
                await _weatherProvider.FindForCoordinatesAsync(currentLocation.Latitude, currentLocation.Longitude, "metric", new CancellationTokenSource());

            IsBusy = false;

            if (CurrentWeather != null)
                SendChangeBackgroundMessageCommand.Execute();
            Forecast =
                await
                    _forecastProvider.FindForCoordinatesAsync(currentLocation.Latitude, currentLocation.Longitude,
                        "metric", new CancellationTokenSource());
        }

        private void SendChangeBackgroundMessage()
        {
            _messenger.Publish(new NeedChangeBackgroundColorMessage(this, CurrentWeather.Clouds.All));
        }
    }
}