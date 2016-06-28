using MvvmCross.Core.ViewModels;
using XWeather.Entities;

namespace XWeather.Dto
{
    public class MainDto : MvxViewModel
    {
        private double _temp;
        private double _pressure;
        private double _humidity;
        private double _tempMin;
        private double _tempMax;

        public MainDto()
        {
            
        }

        public MainDto(Main main)
        {
            Temp = main.temp;
            Pressure = main.pressure;
            Humidity = main.humidity;
            TempMin = main.temp_min;
            TempMax = main.temp_max;
        }

        public double Temp
        {
            get { return _temp; }
            set { _temp = value; RaisePropertyChanged(); }
        }

        public double Pressure
        {
            get { return _pressure; }
            set { _pressure = value; RaisePropertyChanged(); }
        }

        public double Humidity
        {
            get { return _humidity; }
            set { _humidity = value; RaisePropertyChanged(); }
        }

        public double TempMin
        {
            get { return _tempMin; }
            set { _tempMin = value; RaisePropertyChanged(); }
        }

        public double TempMax
        {
            get { return _tempMax; }
            set { _tempMax = value; RaisePropertyChanged(); }
        }
    }
}