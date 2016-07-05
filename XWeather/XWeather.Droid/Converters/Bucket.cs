using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

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
}