using MvvmCross.Core.ViewModels;

namespace XWeather.Dto
{
    public class WeatherDto : MvxViewModel
    {
        private int _id;
        private string _main;
        private string _description;
        private string _icon;

        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(); }
        }

        public string Main
        {
            get { return _main; }
            set { _main = value; RaisePropertyChanged(); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged(); }
        }

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; RaisePropertyChanged(); }
        }
    }
}