using System;
using MvvmCross.Core.ViewModels;

namespace XWeather.Dto
{
    public class DayForecastDto : MvxViewModel
    {
        private double _maxTemp;
        private double _minTemp;
        private double _clouds;
        private DateTime _forecastTime;


        public DayForecastDto()
        {
            
        }

        public DayForecastDto(CurrentWeatherDto weather)
        {
            MaxTemp = weather.Main.TempMax;
            MinTemp = weather.Main.TempMin;
            Clouds = weather.Clouds.All;
            ForecastTime = weather.WeatherDateTime;
        }

        public double MaxTemp
        {
            get { return _maxTemp; }
            set { _maxTemp = value; RaisePropertyChanged(); }
        }

        public double MinTemp
        {
            get { return _minTemp; }
            set { _minTemp = value; RaisePropertyChanged(); }
        }

        public double Clouds
        {
            get { return _clouds; }
            set { _clouds = value; RaisePropertyChanged(); }
        }

        public DateTime ForecastTime
        {
            get { return _forecastTime; }
            set { _forecastTime = value; RaisePropertyChanged(); }
        }
    }
}