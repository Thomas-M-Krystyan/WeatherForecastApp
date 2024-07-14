using WeatherForecastApp.WebApi.Properties;

namespace WeatherForecastApp.WebApi.Responses
{
    /// <summary>
    /// <inheritdoc cref="CommandResult"/>
    /// <para>
    /// The feedback for successful operation.
    /// </para>
    /// </summary>
    internal sealed record CommandResultSucces(int changesCount)
        : CommandResult(true, changesCount, Resource.RESPONSE_Command_Success)
    {
    }
}
