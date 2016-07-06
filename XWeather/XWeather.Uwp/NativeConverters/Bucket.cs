using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
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

    public class NativeDateTimeStringToTimeStringConvertert : MvxNativeValueConverter<DateTimeStringToTimeStringConverter>
    { }

    public class NativeValueMultipliedByFactorConverter : MvxNativeValueConverter<ValueMultipliedByFactorConverter>
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
}