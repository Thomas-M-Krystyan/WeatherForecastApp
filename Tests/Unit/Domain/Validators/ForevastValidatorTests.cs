using System;
using System.Text.Json;
using WeatherForecastApp.Domain.Constants;
using WeatherForecastApp.Domain.Enums;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Domain.Respones;
using WeatherForecastApp.Domain.Validators;

namespace WeatherForecastApp.Domain.Tests.Validators
{
    [TestFixture]
    public sealed class ForevastValidatorTests
    {
        private ForecastValidator _validator = null!;

        [OneTimeSetUp]
        public void SetupTests() => this._validator = new ForecastValidator();

        [Test]
        public void Validate_ForValidForecast_ReturnsTrue_AndExpectedMessage()
        {
            // Arrange
            WeatherForecast forecast = GetForecast();

            // Act & Assert
            TestForecastValidator(forecast, true, "The object is valid.");
        }

        [Test]
        public void Validate_ForInvalidForecast_LocalDateTime_ReturnsFalse_AndExpectedMessage()
        {
            // Arrange
            WeatherForecast forecast = GetForecast(date: DateTime.Now);

            // Act & Assert
            TestForecastValidator(forecast, false, "The date should be in UTC format.");
        }

        [Test]
        public void Validate_ForInvalidForecast_PastDateTime_ReturnsFalse_AndExpectedMessage()
        {
            // Arrange
            WeatherForecast forecast = GetForecast(date: DateTime.UtcNow.AddDays(-5));

            // Act & Assert
            TestForecastValidator(forecast, false, "The date is in the past.");
        }

        [Test]
        public void Validate_ForInvalidForecast_TooCold_ReturnsFalse_AndExpectedMessage()
        {
            // Arrange
            WeatherForecast forecast = GetForecast(tempC: CommonValues.Database.MinAllowedTemp - 1);

            // Act & Assert
            TestForecastValidator(forecast, false, "The temperature is too low.");
        }

        [Test]
        public void Validate_ForInvalidForecast_TooHot_ReturnsFalse_AndExpectedMessage()
        {
            // Arrange
            WeatherForecast forecast = GetForecast(tempC: CommonValues.Database.MaxAllowedTemp + 1);

            // Act & Assert
            TestForecastValidator(forecast, false, "The temperature is too high.");
        }

        [TestCase("")]
        [TestCase(" ")]
        public void Validate_ForInvalidForecast_EmptyDescription_ReturnsFalse_AndExpectedMessage(string invalidDescription)
        {
            // Arrange
            WeatherForecast forecast = GetForecast(description: invalidDescription);

            // Act & Assert
            TestForecastValidator(forecast, false, "The description is missing.");
        }

        #region Helper methods
        private void TestForecastValidator(WeatherForecast forecast, bool isValid, string expectedMessage)
        {
            // Act
            ValidatorResponse actualResult = this._validator.Validate(forecast);

            // Assert
            string actualSerializedResult = JsonSerializer.Serialize(actualResult);
            string expectedSerializedString = $"{{\"IsValid\":{isValid.ToString().ToLower()},\"Message\":\"{expectedMessage}\"}}";

            Assert.That(actualSerializedResult, Is.EqualTo(expectedSerializedString));
        }

        private static WeatherForecast GetForecast(DateTime? date = null, float? tempC = null, float? tempF = null, string? description = null)
        {
            return new WeatherForecast(
                date ?? DateTime.UtcNow.AddMinutes(10),
                tempC ?? 25,
                tempF ?? 77,
                description ?? FeelingTemperature.Balmy.ToString());
        }
        #endregion
    }
}
