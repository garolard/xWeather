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
            Mvx.RegisterSingleton<ICurrentWeatherProvider>(() => new CurrentWeatherProvider());
            Mvx.RegisterSingleton<IForecastProvider>(() => new ForecastProvider());

            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());
        }
    }
}