using Android.App;
using MvvmCross.Droid.Views;

namespace XWeather.Droid
{
	[Activity (Label = "Main", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainView : MvxActivity
	{
	    protected override void OnViewModelSet()
	    {
            SetContentView(Resource.Layout.Main);
	    }
	}
}


