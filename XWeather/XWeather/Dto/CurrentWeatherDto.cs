using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using XWeather.Entities;

namespace XWeather.Dto
{
    public class CurrentWeatherDto : MvxViewModel
    {
        private CoordDto _coordinates;
        private List<WeatherDto> _weather;
        private string _base;
        private MainDto _main;
        private WindDto _wind;
        private CloudsDto _clouds;
        private long _dt;
        private string _weatherTime;
        private SysDto _sys;
        private int _id;
        private string _name;
        private int _cod;

        public CurrentWeatherDto()
        {
            
        }

        public CurrentWeatherDto(CurrentWeather currentWeather)
        {
            Coordinates = currentWeather.coord == null 
                ? new CoordDto() 
                : new CoordDto(currentWeather.coord);
            Base = currentWeather.@base;
            Main = currentWeather.main == null 
                ? new MainDto()
                : new MainDto(currentWeather.main);
            Wind = currentWeather.wind == null 
                ? new WindDto()
                : new WindDto(currentWeather.wind);
            Clouds = currentWeather.clouds == null 
                ? new CloudsDto()
                : new CloudsDto(currentWeather.clouds);
            Dt = currentWeather.dt;
            WeatherTime = currentWeather.dt_txt;
            Sys = currentWeather.sys == null
                ? new SysDto() 
                : new SysDto(currentWeather.sys);
            Id = currentWeather.id;
            Name = currentWeather.name;
            Code = currentWeather.cod;
            Weather = new List<WeatherDto>();
            foreach (var weather in currentWeather.weather)
            {
                Weather.Add(new WeatherDto(weather));
            }
        }

        public CoordDto Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; RaisePropertyChanged(); }
        }

        public List<WeatherDto> Weather
        {
            get { return _weather; }
            set { _weather = value; RaisePropertyChanged(); }
        }

        public string Base
        {
            get { return _base; }
            set { _base = value; RaisePropertyChanged(); }
        }

        public MainDto Main
        {
            get { return _main; }
            set { _main = value; RaisePropertyChanged(); }
        }

        public WindDto Wind
        {
            get { return _wind; }
            set { _wind = value; RaisePropertyChanged(); }
        }

        public CloudsDto Clouds
        {
            get { return _clouds; }
            set { _clouds = value; RaisePropertyChanged(); }
        }

        public long Dt
        {
            get { return _dt; }
            set { _dt = value; RaisePropertyChanged(); }
        }

        public DateTime WeatherDateTime => DateTime.ParseExact(WeatherTime, "yyyy-MM-dd HH:mm:ss", null);

        public string WeatherTime
        {
            get { return _weatherTime; }
            set { _weatherTime = value; RaisePropertyChanged(); }
        }

        public SysDto Sys
        {
            get { return _sys; }
            set { _sys = value; RaisePropertyChanged(); }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }

        public int Code
        {
            get { return _cod; }
            set { _cod = value; RaisePropertyChanged(); }
        }
    }
}