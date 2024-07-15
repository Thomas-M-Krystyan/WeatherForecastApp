using WeatherForecastApp.Domain.Respones;

namespace WeatherForecastApp.Domain.Validators.Interfaces
{
    /// <summary>
    /// A generic interface for validators.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IValidator<TModel>
    {
        /// <summary>
        /// Checks whether the given <typeparamref name="TModel"/> is valid.
        /// </summary>
        /// <param name="model">The model to be validated.</param>
        public ValidatorResponse Validate(TModel model);
    }
}
