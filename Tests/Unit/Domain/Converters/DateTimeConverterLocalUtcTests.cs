using System;
using System.Globalization;
using WeatherForecastApp.Domain.Converters;

namespace WeatherForecastApp.Domain.Tests.Converters
{
    [TestFixture]
    public sealed class DateTimeConverterLocalUtcTests
    {
        [Test]
        public void ConvertFrom_Local_ToUtc_ReturnsValidUtcDateTime()
        {
            // Arrange
            string cetTimeString = "2024-07-15T18:00:00";
            var cetDateTimeOffset = DateTimeOffset.Parse(cetTimeString,
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            var cetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            DateTimeOffset cetTime = TimeZoneInfo.ConvertTime(cetDateTimeOffset, cetTimeZone);
            DateTime cetDateTime = cetTime.DateTime;

            var unitConverter = new DateTimeConverterLocalUtc();

            // Act
            DateTime utcDateTime = unitConverter.ConvertFrom(cetDateTime);

            // Assert
            Assert.That(utcDateTime.ToString(), Is.EqualTo("7/15/2024 04:00:00 PM"));
        }

        [Test]
        public void ConvertBack_Utc_ToLocal_ReturnsValidLocalDateTime()
        {
            // Arrange
            var utcDateTime = new DateTime(2024, 7, 15, 4, 0, 0, DateTimeKind.Utc);

            var unitConverter = new DateTimeConverterLocalUtc();

            // Act
            DateTime cetDateTime = unitConverter.ConvertBack(utcDateTime);

            // Assert
            Assert.That(cetDateTime.ToString(), Is.EqualTo("7/15/2024 06:00:00 PM"));
        }
    }
}
