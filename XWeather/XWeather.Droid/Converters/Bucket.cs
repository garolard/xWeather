using System;
using System.Globalization;
using Android.Content;
using Android.Graphics;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.Droid;

namespace XWeather.Droid.Converters
{
    public class BoolToVisibilityConverter : MvxValueConverter<bool, int>
    {
        protected override int Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            // PLEASE FIND THE CONSTANTS
            if (value)
                return 0;
            else
                return 8;
        }
    }

    public class CloudsToWeatherImage : MvxValueConverter<double, string>
    {
        protected override string Convert(double clouds, Type targetType, object parameter, CultureInfo culture)
        {
            if (clouds >= 0 && clouds < 25)
            {
                return "WeatherImages/WeatherSun.png";
            }
            else if (clouds >= 25 && clouds < 75)
            {
                return "WeatherImages/WeatherPartlyCloud.png";
            }
            else
            {
                return "WeatherImages/WeatherClouds.png";
            }
        }
    }
}