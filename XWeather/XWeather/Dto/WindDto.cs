using MvvmCross.Core.ViewModels;
using XWeather.Entities;

namespace XWeather.Dto
{
    public class WindDto : MvxViewModel
    {
        private double _speed;
        private double _deg;

        public WindDto()
        {
            
        }

        public WindDto(Wind wind)
        {
            Speed = wind.speed;
            Deg = wind.deg;
        }

        public double Speed
        {
            get { return _speed; }
            set { _speed = value; RaisePropertyChanged(); }
        }

        public double Deg
        {
            get { return _deg; }
            set { _deg = value; RaisePropertyChanged(); }
        }

    }
}