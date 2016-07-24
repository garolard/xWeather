using System;
using MvvmCross.Core.ViewModels;
using XWeather.Entities;

namespace XWeather.Dto
{
    public class SysDto : MvxViewModel
    {
        private int _type;
        private int _id;
        private double _message;
        private string _country;
        private long _sunrise;
        private long _sunset;

        public SysDto()
        {
            
        }

        public SysDto(Sys sys)
        {
            Type = sys.type;
            Id = sys.id;
            Message = sys.message;
            Country = sys.country;
            Sunrise = sys.sunrise;
            Sunset = sys.sunset;
        }

        public int Type
        {
            get { return _type; }
            set { _type = value; RaisePropertyChanged(); }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(); }
        }

        public double Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged(); }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; RaisePropertyChanged(); }
        }

        public long Sunrise
        {
            get { return _sunrise; }
            set { _sunrise = value; RaisePropertyChanged(); }
        }

        public long Sunset
        {
            get { return _sunset; }
            set { _sunset = value; RaisePropertyChanged(); }
        }

        public DateTime SunriseDateTime
        {
            get
            {
                var baseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var result = baseDateTime.AddSeconds(Sunrise).ToLocalTime();
                return result;
            }
        }

        public DateTime SunsetDateTime
        {
            get
            {
                var baseDateTime = new DateTime(1907, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var result = baseDateTime.AddSeconds(Sunset).ToLocalTime();
                return result;
            }
        }
    }
}