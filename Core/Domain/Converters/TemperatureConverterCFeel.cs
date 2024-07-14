﻿using System;
using System.Collections.Generic;
using WeatherForecastApp.Domain.Constants;
using WeatherForecastApp.Domain.Converters.Interfaces;
using WeatherForecastApp.Domain.Enums;
using WeatherForecastApp.Domain.Models.Units;
using WeatherForecastApp.Domain.Utilities;

namespace WeatherForecastApp.Domain.Converters
{
    /// <summary>
    /// <inheritdoc cref="IUnitConverter{TUnitA, TUnitB}"/>
    /// <para>
    /// Implements a converter between <see cref="TemperatureCelsius"/> and <see cref="FeelingTemperature"/>.
    /// </para>
    /// </summary>
    internal sealed class TemperatureConverterCFeel : IUnitConverter<TemperatureCelsius, FeelingTemperature>
    {
        private const float AbsoluteZero = -273.15f;

        private static readonly Dictionary<FeelingTemperature, (short Min, short Max)> NameToTempsMap =
            new Dictionary<FeelingTemperature, (short, short)>()
        {
            { FeelingTemperature.Freezing,   ((short)CommonValues.Database.MinAllowedTemp, -48) },
            { FeelingTemperature.Bracing,    (-48, -36) },
            { FeelingTemperature.Chilly,     (-36, -24) },
            { FeelingTemperature.Cool,       (-24, -12) },
            { FeelingTemperature.Mild,       (-12,   0) },
            { FeelingTemperature.Warm,       (  0,  12) },
            { FeelingTemperature.Balmy,      ( 12,  24) },
            { FeelingTemperature.Hot,        ( 24,  36) },
            { FeelingTemperature.Sweltering, ( 36,  48) },
            { FeelingTemperature.Scorching,  ( 48,  (short)CommonValues.Database.MaxAllowedTemp) },
        };

        /// <inheritdoc cref="IRandomGenerator"/>
        internal IRandomGenerator RandomGenerator { get; set; } = new RandomGenerator();

        /// <inheritdoc cref="IUnitConverter{TUnitA, TUnitB}.ConvertFrom(TUnitA)"/>
        public FeelingTemperature ConvertFrom(TemperatureCelsius unit)
        {
            // NOTE: This solution is easier to maintain and have control by human (e.g., how wide are the temperature ranges).
            //       An alternative would be programatically calculated "steps" between different "feeling temperature" categories
            //       determined from Enum.GetNames(typeof(TEnum)).Length - 1 (to skip "Unknown") and stored in dictionary, e.g. =>
            //       0.00, Warm
            //       0.01, Warm
            //       0.02, Warm
            //       ...after 12 degrees (which is one step => calculation: (Abs(minTemp) + maxTemp) / 10 categories * 100 precision)
            //          an another temperature would be mapped
            //       12.00, Balmy
            //       12.01, Balmy
            //       ... and so on
            //       TimeComplexity would be much better O(n), SpaceComplexity O(n^2), maintenance => 0, readability => difficult

            return unit.Value switch
            {
                // Immediate handling of invalid results
                float temp when float.IsNaN(temp)      || temp <= AbsoluteZero ||
                                float.IsInfinity(temp) || temp >= float.MaxValue => FeelingTemperature.Unknown,

                float temp when temp <  CommonValues.Database.MinAllowedTemp => FeelingTemperature.Freezing,
                float temp when temp >= CommonValues.Database.MaxAllowedTemp => FeelingTemperature.Scorching,

                // Iterative trials to determine the value
                float temp when IsInRange(temp, FeelingTemperature.Freezing)   => FeelingTemperature.Freezing,
                float temp when IsInRange(temp, FeelingTemperature.Bracing)    => FeelingTemperature.Bracing,
                float temp when IsInRange(temp, FeelingTemperature.Chilly)     => FeelingTemperature.Chilly,
                float temp when IsInRange(temp, FeelingTemperature.Cool)       => FeelingTemperature.Cool,
                float temp when IsInRange(temp, FeelingTemperature.Mild)       => FeelingTemperature.Mild,
                float temp when IsInRange(temp, FeelingTemperature.Warm)       => FeelingTemperature.Warm,
                float temp when IsInRange(temp, FeelingTemperature.Balmy)      => FeelingTemperature.Balmy,
                float temp when IsInRange(temp, FeelingTemperature.Hot)        => FeelingTemperature.Hot,
                float temp when IsInRange(temp, FeelingTemperature.Sweltering) => FeelingTemperature.Sweltering,
                float temp when IsInRange(temp, FeelingTemperature.Scorching)  => FeelingTemperature.Scorching,

                // Fallback scenario
                _ => FeelingTemperature.Unknown,
            };

            static bool IsInRange(float temperature, FeelingTemperature description)
            {
                return temperature >= NameToTempsMap[description].Min &&
                       temperature <  NameToTempsMap[description].Max;
            }
        }

        /// <inheritdoc cref="IUnitConverter{TUnitA, TUnitB}.ConvertBack(TUnitB)"/>
        public TemperatureCelsius ConvertBack(FeelingTemperature unit)
        {
            if (!Enum.IsDefined(typeof(FeelingTemperature), unit))
            {
                return default;
            }

            // NOTE: This is only a proposition of a solution. Other one would be to throw NotImplementedException.
            //       It's difficult to determine a specific temperature from a broad range of temperatures, based
            //       just on a single input - such as "feeling temperature"

            return new TemperatureCelsius(
                this.RandomGenerator.Next(
                    minValue: NameToTempsMap[unit].Min,        // Inclusive bound
                    maxValue: NameToTempsMap[unit].Max + 1));  // Exclusive bound
        }
    }
}
