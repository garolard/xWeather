using MvvmCross.Platform.WindowsCommon.Converters;
using XWeather.Converters;

namespace XWeather.Uwp.NativeConverters
{
    public class NativeTemperatureConverter : MvxNativeValueConverter<TemperatureConverter>
    { }

    public class NativeToUpperConverter : MvxNativeValueConverter<ToUpperConverter>
    { }
}