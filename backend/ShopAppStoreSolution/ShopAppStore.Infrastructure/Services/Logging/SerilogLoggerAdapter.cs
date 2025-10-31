using Microsoft.Extensions.Logging;
using ShopAppStore.Domain.Interfaces.Logging;

namespace ShopAppStore.Infrastructure.Services.Logging
{
    public class SerilogLoggerAdapter<T>(ILogger<T> logger) : IAppLogger<T>
    {
        public void LogError(string message, Exception ex)
        {
            logger.LogError(ex, message);
        }

        public void LogInformation(string message)
        {
            logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            logger.LogWarning(message);
        }
    }
}
