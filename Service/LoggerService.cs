using WebAPICoreSQL.Data;
using WebAPICoreSQL.Model;

namespace WebAPICoreSQL.Service
{
    public class LoggerService : ILoggerService
    {
        private static LoggerService? instance;
        private static readonly object _lock = new object();
        private readonly AppDbContext _context;


        // constructor to prevent instantiation
        public LoggerService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        //Method to log message
        public async Task LogAsync(Guid correlationId, string applicationName, string methodName, string message, string exception = null)
        {
            var LogEntry = new Log
            {
                CorrelationID = correlationId,
                ApplicationName = applicationName,
                MethodName = methodName,
                Message = message,
                Exception = exception
            };
            _context.Log.Add(LogEntry);
            await _context.SaveChangesAsync();

        }

    }
}
