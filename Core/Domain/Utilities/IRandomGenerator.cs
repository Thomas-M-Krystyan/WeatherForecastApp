using System;

namespace WeatherForecastApp.Domain.Utilities
{
    /// <summary>
    /// A service providing random numbers.
    /// </summary>
    internal interface IRandomGenerator
    {
        private static readonly object Padlock = new object();
        
        [ThreadStatic]
        private static readonly Random Random = new Random();

        /// <summary>
        /// <see cref="Random.Next(int, int)"/>.
        /// </summary>
        internal int Next(int minValue, int maxValue)
        {
            lock (Padlock)
            {
                return Random.Next(minValue, maxValue);
            }
        }
    }

    /// <inheritdoc cref="IRandomGenerator"/>
    internal sealed class RandomGenerator : IRandomGenerator { }
}
