using System;
using System.Threading.Tasks;
using NUnit.Framework;
using XWeather.Providers;

namespace XWeather.Tests.Providers
{
    [TestFixture]
    public class CurrentWeatherProviderTests
    {
        [Test]
        public async Task CurrentWeather_NoCityNameProvided_ShouldReturnNull()
        {
            var provider = new CurrentWeatherProvider();
            var currentWeather = await provider.FindForCityNameAsync(string.Empty);
            Assert.IsNull(currentWeather);
        }

        [Test]
        public async Task CurrenWeather_NoCityCodeProvided_ShouldReturnNull()
        {
            var provider = new CurrentWeatherProvider();
            var currentWeather = await provider.FindForCityCodeAsync(string.Empty);
            Assert.IsNull(currentWeather);
        }

        [Test]
        public async Task CurrentWeather_NoLocationProvider_ShouldReturnNull()
        {
            var provider = new CurrentWeatherProvider();
            var currentWeather = await provider.FindForCoordinatesAsync(-1, -1);
            Assert.IsNull(currentWeather);
        }

        [Test]
        public async Task CurrentWeather_MadridCityNameProvided_ShouldReturnWeather()
        {
            var provider = new CurrentWeatherProvider();
            var currentMadridWeather = await provider.FindForCityNameAsync("Madrid,ES");
            Assert.IsNotNull(currentMadridWeather);
        }

        [Test]
        public async Task CurrentWeather_MadridCityCodeProvided_ShouldReturnWeather()
        {
            var provider = new CurrentWeatherProvider();
            var currentMadridWeather = await provider.FindForCityCodeAsync("6359304");
            Assert.IsNotNull(currentMadridWeather);
        }
    }
}