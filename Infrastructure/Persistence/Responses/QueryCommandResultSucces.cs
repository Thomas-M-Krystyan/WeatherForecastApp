using WeatherForecastApp.Persistence.Properties;

namespace WeatherForecastApp.Persistence.Responses
{
    /// <summary>
    /// <inheritdoc cref="QueryCommandResult"/>
    /// <para>
    /// The feedback for successful operation.
    /// </para>
    /// </summary>
    public sealed record QueryCommandResultSucces(int changesCount)
        : QueryCommandResult(true, changesCount, Resource.RESPONSE_Command_Success)
    {
    }
}
