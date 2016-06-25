using MvvmCross.Core.ViewModels;

namespace XWeather.Dto
{
    public class CloudsDto : MvxViewModel
    {
        private int _all;

        public int All
        {
            get { return _all; }
            set { _all = value; RaisePropertyChanged(); }
        }
    }
}