using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using XWeather.Entities;

namespace XWeather.Dto
{
    public class ForecastDto : MvxViewModel
    {
        private CityDto _city;
        private string _cod;
        private double _message;
        private int _cnt;
        private List<CurrentWeatherDto> _list;

        public ForecastDto()
        {
            
        }

        public ForecastDto(Forecast forecast)
        {
            City = new CityDto(forecast.city);
            Code = forecast.cod;
            Message = forecast.message;
            Count = forecast.cnt;
            List = new List<CurrentWeatherDto>();
            foreach (var currentWeather in forecast.list)
            {
                List.Add(new CurrentWeatherDto(currentWeather));
            }
        }


        public CityDto City
        {
            get { return _city; }
            set { _city = value; RaisePropertyChanged(); }
        }

        public string Code
        {
            get { return _cod; }
            set { _cod = value; RaisePropertyChanged(); }
        }

        public double Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged(); }
        }

        public int Count
        {
            get { return _cnt; }
            set { _cnt = value; RaisePropertyChanged(); }
        }

        public List<CurrentWeatherDto> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(); }
        }
    }
}