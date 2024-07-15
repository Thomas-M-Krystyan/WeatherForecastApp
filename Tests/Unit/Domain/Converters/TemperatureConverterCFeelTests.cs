using Moq;
using WeatherForecastApp.Domain.Converters;
using WeatherForecastApp.Domain.Enums;
using WeatherForecastApp.Domain.Models.Units;
using WeatherForecastApp.Domain.Utilities;

namespace WeatherForecastApp.Domain.Tests.Converters
{
    [TestFixture]
    public sealed class TemperatureConverterCFeelTests
    {
        // Invalid cases
        [TestCase(float.NaN, FeelingTemperature.Unknown)]
        [TestCase(float.MinValue, FeelingTemperature.Unknown)]
        [TestCase(float.MaxValue, FeelingTemperature.Unknown)]
        [TestCase(float.NegativeInfinity, FeelingTemperature.Unknown)]
        [TestCase(float.PositiveInfinity, FeelingTemperature.Unknown)]
        [TestCase(-273.16f, FeelingTemperature.Unknown)]
        // Beyond ranges
        [TestCase(-75, FeelingTemperature.Freezing)]
        [TestCase(75, FeelingTemperature.Scorching)]
        // Within ranges
        [TestCase(-60.0f, FeelingTemperature.Freezing)]
        [TestCase(-59.9f, FeelingTemperature.Freezing)]
        [TestCase(0.0f, FeelingTemperature.Freezing)]
        [TestCase(float.NegativeZero, FeelingTemperature.Freezing)]
        [TestCase(float.Epsilon, FeelingTemperature.Freezing)]
        [TestCase(60.0f, FeelingTemperature.Scorching)]
        public void ConvertFrom_C_ToFeelingTemperature_ReturnsExpectedEnum(float testTemperature, FeelingTemperature expectedFeeling)
        {
            // Arrange
            var sourceUnit = new TemperatureCelsius(testTemperature);
            var converter = new TemperatureConverterCFeel();

            // Act
            FeelingTemperature actualFeeling = converter.ConvertFrom(sourceUnit);

            // Assert
            Assert.That(actualFeeling, Is.EqualTo(expectedFeeling));
        }

        [Test]
        public void ConvertFrom_FeelingTemperature_ToC_ReturnsExpectedRange()
        {
            // Arrange
            const int PseudoRandomNumber = 17;

            var mockedRandomGenerator = new Mock<IRandomGenerator>();
            mockedRandomGenerator
                .Setup(mock => mock.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(PseudoRandomNumber);

            var converter = new TemperatureConverterCFeel
            {
                RandomGenerator = mockedRandomGenerator.Object
            };

            // Act
            TemperatureCelsius actualUnit = converter.ConvertBack(FeelingTemperature.Cool);

            // Assert
            Assert.That(actualUnit.Value, Is.EqualTo(PseudoRandomNumber));
        }
    }
}
