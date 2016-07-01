using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using XWeather.Providers;
using XWeather.ViewModels;

namespace XWeather
{
    public class App : MvxApplication
    {
        public App()
        {
            Mvx.RegisterType<ICurrentWeatherProvider, CurrentWeatherProvider>();
            Mvx.RegisterType<IForecastProvider, ForecastProvider>();

            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());
        }
    }
}