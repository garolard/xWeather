using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Support.V7.RecyclerView;
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

        protected override IEnumerable<Assembly> AndroidViewAssemblies
        {
            get
            {
                var assemblies = base.AndroidViewAssemblies;
                ((IList<Assembly>)assemblies).Add(typeof(MvxRecyclerView).Assembly);
                return assemblies;
            }
        }
    }
}