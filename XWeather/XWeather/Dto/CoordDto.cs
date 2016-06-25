using MvvmCross.Core.ViewModels;

namespace XWeather.Dto
{
    public class CoordDto : MvxViewModel
    {
        private double _longitude;
        private double _latitude;


        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; RaisePropertyChanged(); }
        }

        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; RaisePropertyChanged(); }
        }
    }
}