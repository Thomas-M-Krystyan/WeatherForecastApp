using System;
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
            var initialUtcDateTime = new DateTime(2024, 7, 15, 16, 0, 0, DateTimeKind.Utc);
            var cetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            DateTime cetDateTime = TimeZoneInfo.ConvertTimeFromUtc(initialUtcDateTime, cetTimeZone);

            var unitConverter = new DateTimeConverterLocalUtc();

            // Act
            DateTime utcDateTime = unitConverter.ConvertFrom(cetDateTime);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(cetDateTime.Hour, Is.EqualTo(18), "CET time");
                Assert.That(utcDateTime.Hour, Is.EqualTo(16), "UTC time");
            });
            TestContext.WriteLine(cetDateTime);
        }

        [Test]
        public void ConvertBack_Utc_ToLocal_ReturnsValidLocalDateTime()
        {
            // Arrange
            var utcDateTime = new DateTime(2024, 7, 15, 16, 0, 0, DateTimeKind.Utc);

            var unitConverter = new DateTimeConverterLocalUtc();

            // Act
            DateTime cetDateTime = unitConverter.ConvertBack(utcDateTime);

            // Assert
            Assert.That(cetDateTime.Hour, Is.Not.EqualTo(utcDateTime.Hour));
            TestContext.WriteLine(cetDateTime);
        }
    }
}
