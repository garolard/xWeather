using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using XWeather.Dto;
using XWeather.Providers;

namespace XWeather.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly ICurrentWeatherProvider _weatherProvider;

        private string _cityName;
        private CurrentWeatherDto _currentWeather;


        public MainViewModel()
        {
            _weatherProvider = Mvx.Resolve<ICurrentWeatherProvider>();

            CurrentWeather = new CurrentWeatherDto();

            GetCurrentWeatherCommand = new MvxAsyncCommand(GetCurrentWeatherAsync);
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


        private async Task GetCurrentWeatherAsync()
        {
            CurrentWeather =
                await _weatherProvider.FindForCityNameAsync(CityName, "metric", new CancellationTokenSource());
        }
    }
}