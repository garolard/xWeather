using System;
using System.Globalization;
using Windows.Globalization.DateTimeFormatting;
using MvvmCross.Platform.Converters;
using Windows.System.UserProfile;

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

    public class DateTimeToTimeConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString("t");
        }

        protected override DateTime ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.ParseExact(value, "hh:mm", null);
        }
    }

    public class DateTimeStringToTimeStringConverter : MvxValueConverter<string>
    {
        protected override object Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Split(' ')[1].Substring(0, 5);
        }
    }

    public class ValueMultipliedByFactorConverter : MvxValueConverter<double, double>
    {
        protected override double Convert(double value, Type targetType, object parameter, CultureInfo culture)
        {
            double factor;
            var couldParse = double.TryParse(parameter.ToString(), out factor);

            if (couldParse)
                return value*factor;
            else
                return value;
        }
    }

    public class DateTimeToDayNameConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            var dayOfWeek = value.DayOfWeek;
            var language = new DateTimeFormatter("longdate", new []{"US"}).ResolvedLanguage;
            var c = new CultureInfo(language);
            return c.DateTimeFormat.GetAbbreviatedDayName(dayOfWeek);
        }
    }
}