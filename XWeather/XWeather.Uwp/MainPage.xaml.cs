using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using MvvmCross.WindowsUWP.Views;
using XWeather.Messages;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace XWeather.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : MvxWindowsPage
    {
        private MvxSubscriptionToken _changeBrackgroundSubscriptionToken;
        private Color _backgroundColor;


        public MainPage()
        {
            this.InitializeComponent();
            var messenger = Mvx.Resolve<IMvxMessenger>();
            _changeBrackgroundSubscriptionToken =
                messenger.SubscribeOnMainThread<NeedChangeBackgroundColorMessage>(ChangeBackgroundColor);
        }

        private void ChangeBackgroundColor(NeedChangeBackgroundColorMessage obj)
        {
            var clouds = obj.Clouds;

            if (clouds < 25)
            {
                _backgroundColor = (Color)Application.Current.Resources["SunnyBlue"];
            }
            else if (clouds >= 25 && clouds < 75)
            {
                _backgroundColor = (Color)Application.Current.Resources["PartlyCloudyBlue"];
            }
            else
            {
                _backgroundColor = (Color)Application.Current.Resources["CloudyGray"];
            }

            ColorAnimation.To = _backgroundColor;
            ChangeBackgroundStoryboard.Begin();
        }
    }
}
