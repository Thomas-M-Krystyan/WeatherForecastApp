using System.Text.Json.Serialization;
using WeatherForecastApp.Domain.Properties;

namespace WeatherForecastApp.Domain.Respones
{
    /// <summary>
    /// The response from validator.
    /// </summary>
    public readonly struct ValidatorResponse
    {
        /// <summary>
        /// The confirming validation result.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// The denying validation result.
        /// </summary>
        [JsonIgnore]
        public bool IsInvalid => !this.IsValid;

        /// <summary>
        /// The validation message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorResponse"/> struct.
        /// </summary>
        /// <param name="isValid"><inheritdoc cref="IsValid" path="/summary"/></param>
        /// <param name="message"><inheritdoc cref="Message" path="/summary"/></param>
        private ValidatorResponse(bool isValid, string message)
        {
            this.IsValid = isValid;
            this.Message = message;
        }

        /// <summary>
        /// Positive result of validation.
        /// </summary>
        public static ValidatorResponse Valid()
            => new ValidatorResponse(true, Resources.Validation_Success);

        /// <summary>
        /// Negative result of validation.
        /// </summary>
        /// <param name="message">The message to be used.</param>
        public static ValidatorResponse Invalid(string message)
            => new ValidatorResponse(false, message);
    }
}
