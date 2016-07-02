using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
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
                RootContainer.Background = Application.Current.Resources["SunnyBrush"] as SolidColorBrush;
            }
            else if (clouds >= 25 && clouds < 75)
            {
                RootContainer.Background = Application.Current.Resources["PatlyCloudyBrush"] as SolidColorBrush;
            }
            else
            {
                RootContainer.Background = Application.Current.Resources["CloudyBrush"] as SolidColorBrush;
            }
        }
    }
}
