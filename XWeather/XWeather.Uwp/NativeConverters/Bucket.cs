using System;
using System.Globalization;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.WindowsCommon.Converters;
using XWeather.Converters;

namespace XWeather.Uwp.NativeConverters
{
    public class NativeTemperatureConverter : MvxNativeValueConverter<TemperatureConverter>
    { }

    public class NativeToUpperConverter : MvxNativeValueConverter<ToUpperConverter>
    { }

    public class NativeDateTimeToTimeConverter : MvxNativeValueConverter<DateTimeToTimeConverter>
    { }

    public class NativeDateTimeStringToTimeStringConverter : MvxNativeValueConverter<DateTimeStringToTimeStringConverter>
    { }

    public class NativeValueMultipliedByFactorConverter : MvxNativeValueConverter<ValueMultipliedByFactorConverter>
    { }

    public class NativeDateTimeToDayNameConverter : MvxNativeValueConverter<DateTimeToDayNameConverter>
    { }

    public class NativeCustomSuffixedDoubleConverter : MvxNativeValueConverter<CustomSuffixedDoubleConverter>
    { }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool) value)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (((Visibility) value) == Visibility.Visible)
                return true;
            else
                return false;
        }
    }

    public class CloudsToWeatherImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var clouds = (double)value;
            if (clouds >= 0 && clouds < 25)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/WeatherImages/WeatherSun.png", UriKind.Absolute));
            }
            else if (clouds >= 25 && clouds < 75)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/WeatherImages/WeatherPartlyCloud.png", UriKind.Absolute));
            }
            else
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/WeatherImages/WeatherClouds.png"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}