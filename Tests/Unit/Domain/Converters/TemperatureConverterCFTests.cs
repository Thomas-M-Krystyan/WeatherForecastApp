using WeatherForecastApp.Domain.Converters;
using WeatherForecastApp.Domain.Models.Units;

namespace WeatherForecastApp.Domain.Tests.Converters
{
    [TestFixture]
    public sealed class TemperatureConverterCFTests
    {
        [Test]
        public void ConvertFrom_C_ToF_ReturnsValidFahrenheit()
        {
            // Arrange
            const float DegreesC = 37.0f;
            const float DegreesF = 98.6f;

            var sourceUnit = new TemperatureCelsius(DegreesC);
            var unitConverter = new TemperatureConverterCF();

            // Act
            TemperatureFahrenheit convertedUnit = unitConverter.ConvertFrom(sourceUnit);

            // Assert
            Assert.That(convertedUnit.Value, Is.EqualTo(DegreesF));
        }

        [Test]
        public void ConvertBack_F_ToC_ReturnsValidCelsius()
        {
            // Arrange
            const float DegreesF = 98.6f;
            const float DegreesC = 37.0f;

            var sourceUnit = new TemperatureFahrenheit(DegreesF);
            var unitConverter = new TemperatureConverterCF();

            // Act
            TemperatureCelsius convertedUnit = unitConverter.ConvertBack(sourceUnit);

            // Assert
            Assert.That(convertedUnit.Value, Is.EqualTo(DegreesC));
        }
    }
}
