using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using XWeather.Messages;

namespace XWeather.Droid
{
	[Activity (Label = "Main", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainView : MvxActivity
	{
	    private MvxSubscriptionToken _token;
	    private IMvxMessenger _messenger;

	    protected override void OnViewModelSet()
	    {
            SetContentView(Resource.Layout.Main);

	        SetUpHorizontalForecastList();

	        _messenger = Mvx.Resolve<IMvxMessenger>();
	        _token = _messenger.SubscribeOnMainThread<NeedChangeBackgroundColorMessage>(ChangeBackgroundColor);
	    }

	    private void SetUpHorizontalForecastList()
	    {
	        var layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
	        var recycler = FindViewById<MvxRecyclerView>(Resource.Id.ForecastList);
            recycler.SetLayoutManager(layoutManager);
	    }

	    private void ChangeBackgroundColor(NeedChangeBackgroundColorMessage obj)
	    {
	        var clouds = obj.Clouds;
	        var container = FindViewById<RelativeLayout>(Resource.Id.MainContainer);
	        var originColor = new ColorDrawable(Color.White);
	        ColorDrawable destColor;

            if (clouds < 25)
            {
                destColor = new ColorDrawable(Resources.GetColor(Resource.Color.SunnyBlue));
            }
            else if (clouds >= 25 && clouds < 75)
            {
                destColor = new ColorDrawable(Resources.GetColor(Resource.Color.PartlyCloudyBlue));
            }
            else
            {
                destColor = new ColorDrawable(Resources.GetColor(Resource.Color.CloudyGray));
            }

            var drawable = new TransitionDrawable(new Drawable[] {originColor, destColor});
	        container.Background = drawable;
            drawable.StartTransition(250);
	    }
	}
}


