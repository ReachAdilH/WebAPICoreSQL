namespace WebAPICoreSQL.Service
{
    public interface ILoggerService
    {
        Task LogAsync(Guid correlationId, string applicationName, string methodName, string message, string exception = null);
    }
}
