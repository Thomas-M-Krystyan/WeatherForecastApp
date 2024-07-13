using Moq;
using System;
using WeatherForecastApp.Domain.Converters;
using WeatherForecastApp.Domain.Converters.Interfaces;
using WeatherForecastApp.Domain.Models.Units;
using WeatherForecastApp.Domain.Resolvers;
using WeatherForecastApp.Domain.Resolvers.Interfaces;

namespace WeatherForecastApp.Domain.Tests.Resolvers
{
    [TestFixture]
    public sealed class UnitConverterResolverTests
    {
        private readonly Mock<IServiceProvider> _serviceProviderMock = new(MockBehavior.Strict);
        private readonly Mock<IServiceHandler> _serviceHandlerMock = new(MockBehavior.Strict);

        [TearDown]
        public void CleanUpTests()
        {
            this._serviceProviderMock.Reset();
            this._serviceHandlerMock.Reset();
        }

        [Test]
        public void Resolve_ForService_Registered_ReturnsRegisteredService()
        {
            // Arrange
            this._serviceProviderMock
                .Setup(mock => mock.GetService(It.IsAny<Type>()))
                .Returns(new TemperatureConverterCF());

            var resolver = new ServiceResolver(
                this._serviceProviderMock.Object,
                this._serviceHandlerMock.Object);

            // Act
            IUnitConverter<TemperatureCelsius, TemperatureFahrenheit> actualResult = resolver.Resolve<TemperatureConverterCF>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualResult, Is.Not.Null);

                this._serviceProviderMock.Verify(mock =>
                    mock.GetService(It.IsAny<Type>()),
                    Times.Once);
            });
        }

        [Test]
        public void Resolve_ForService_NotRegistered_Cached_ReturnsCachedService()
        {
            // Arrange
            this._serviceProviderMock
                .Setup(mock => mock.GetService(It.IsAny<Type>()))
                .Returns(new object());  // Invalid condition

            object converter = new TemperatureConverterCF();
            this._serviceHandlerMock
                .Setup(mock => mock.GetCachedService<TemperatureConverterCF>(out converter))
                .Returns(true);

            var resolver = new ServiceResolver(
                this._serviceProviderMock.Object,
                this._serviceHandlerMock.Object);

            // Act
            IUnitConverter<TemperatureCelsius, TemperatureFahrenheit> actualResult = resolver.Resolve<TemperatureConverterCF>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualResult, Is.Not.Null);

                this._serviceProviderMock.Verify(mock =>
                    mock.GetService(It.IsAny<Type>()),
                    Times.Once);

                this._serviceHandlerMock.Verify(mock =>
                    mock.GetCachedService<TemperatureConverterCF>(out It.Ref<object>.IsAny),
                    Times.Once);
            });
        }

        [Test]
        public void Resolve_ForService_NotRegistered_NotCached_ReturnsNewlyInstantiatedService()
        {
            // Arrange
            this._serviceProviderMock
                .Setup(mock => mock.GetService(It.IsAny<Type>()))
                .Returns(new object());  // Invalid condition

            this._serviceHandlerMock
                .Setup(mock => mock.GetCachedService<TemperatureConverterCF>(out It.Ref<object>.IsAny))
                .Returns(false);  // Invalid condition
            this._serviceHandlerMock
                .Setup(mock => mock.CreateService<TemperatureConverterCF>())
                .Returns(new TemperatureConverterCF());
            this._serviceHandlerMock
                .Setup(mock => mock.CacheService(It.IsAny<object>()));

            var resolver = new ServiceResolver(
                this._serviceProviderMock.Object,
                this._serviceHandlerMock.Object);

            // Act
            IUnitConverter<TemperatureCelsius, TemperatureFahrenheit> actualResult = resolver.Resolve<TemperatureConverterCF>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualResult, Is.Not.Null);

                this._serviceProviderMock.Verify(mock =>
                    mock.GetService(It.IsAny<Type>()),
                    Times.Once);

                this._serviceHandlerMock.Verify(mock =>
                    mock.GetCachedService<TemperatureConverterCF>(out It.Ref<object>.IsAny),
                    Times.Once);

                this._serviceHandlerMock
                    .Verify(mock => mock.CreateService<TemperatureConverterCF>(),
                    Times.Once);

                this._serviceHandlerMock
                    .Verify(mock => mock.CacheService(It.IsAny<object>()),
                    Times.Once);
            });
        }
    }
}
