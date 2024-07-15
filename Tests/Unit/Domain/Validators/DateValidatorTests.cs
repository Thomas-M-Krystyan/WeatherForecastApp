using System;
using System.Text.Json;
using WeatherForecastApp.Domain.Respones;
using WeatherForecastApp.Domain.Validators;

namespace WeatherForecastApp.Domain.Tests.Validators
{
    [TestFixture]
    public sealed class DateValidatorTests
    {
        private DateValidator _validator = null!;

        [OneTimeSetUp]
        public void SetupTests() => this._validator = new DateValidator();

        [Test]
        public void Validate_ForValidDateTime_ReturnsTrue_AndExpectedMessage()
        {
            // Arrange
            DateTime dateTime = DateTime.UtcNow.AddMinutes(10);

            // Act & Assert
            TestForecastValidator(dateTime, true, "The object is valid.");
        }

        [Test]
        public void Validate_ForInvalidDateTime_Local_ReturnsFalse_AndExpectedMessage()
        {
            // Arrange
            DateTime dateTime = DateTime.Now;

            // Act & Assert
            TestForecastValidator(dateTime, false, "The date should be in UTC format.");
        }

        [Test]
        public void Validate_ForInvalidDateTime_Past_ReturnsFalse_AndExpectedMessage()
        {
            // Arrange
            DateTime dateTime = DateTime.UtcNow.AddDays(-5);

            // Act & Assert
            TestForecastValidator(dateTime, false, "The date is in the past.");
        }

        #region Helper methods
        private void TestForecastValidator(DateTime dateTime, bool isValid, string expectedMessage)
        {
            // Act
            ValidatorResponse actualResult = this._validator.Validate(dateTime);

            // Assert
            string actualSerializedResult = JsonSerializer.Serialize(actualResult);
            string expectedSerializedString = $"{{\"IsValid\":{isValid.ToString().ToLower()},\"Message\":\"{expectedMessage}\"}}";

            Assert.That(actualSerializedResult, Is.EqualTo(expectedSerializedString));
        }
        #endregion
    }
}
