using System.Threading.Tasks;
using NUnit.Framework;
using XWeather.Providers;

namespace XWeather.Tests.Providers
{
    [TestFixture]
    public class ForecastProviderTests
    {
        [Test]
        public async Task FiveDaysForecast_NoCityCodeProvided_ShouldReturnNull()
        {
            var provider = new ForecastProvider();
            var forecast = await provider.FindForCityCodeAsync(string.Empty);
            Assert.IsNull(forecast);
        }

        [Test]
        public async Task FiveDaysForecast_NoCitynameProvided_ShouldReturnNull()
        {
            var provider = new ForecastProvider();
            var forecast = await provider.FindForCityNameAsync(string.Empty);
            Assert.IsNull(forecast);
        }

        [Test]
        public async Task FiveDaysForecast_MadridCityCodeProvided_ShouldReturnForecast()
        {
            var provider = new ForecastProvider();
            var forecast = await provider.FindForCityCodeAsync("6359304");
            Assert.IsNotNull(forecast);
        }

        [Test]
        public async Task FiveDaysForecast_MadridCityNameProvided_ShouldReturnForecast()
        {
            var provider = new ForecastProvider();
            var forecast = await provider.FindForCityNameAsync("Madrid,ES");
            Assert.IsNotNull(forecast);
        }
    }
}