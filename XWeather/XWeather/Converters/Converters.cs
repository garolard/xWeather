using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace XWeather.Converters
{
    public class TemperatureConverter : MvxValueConverter<double, string>
    {
        protected override string Convert(double value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round(value).ToString("00") + "º C";
        }

        protected override double ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.Parse(value);
        }
    }

    public class ToUpperConverter : MvxValueConverter<string>
    {
        protected override object Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty : value.ToUpper();
        }
    }
}