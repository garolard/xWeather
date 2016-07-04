using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using XWeather.Droid.Providers;
using XWeather.Providers;

namespace XWeather.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterSingleton<ILocationProvider>(() => new AndroidLocationProvider());
            return new App();
        }
    }
}