using Microsoft.Extensions.Logging;
using ZLogger;

namespace Core.Logging
{
    public static class LoggerExtensions
    {
        public static void ZLogWithCategory(this ILogger logger, LogLevel level, LogCategory category, string message)
        {
            string tagged = $"[{category}] {message}";
            switch (level)
            {
                case LogLevel.Trace: logger.ZLogTrace($"{tagged}"); break;
                case LogLevel.Debug: logger.ZLogDebug($"{tagged}"); break;
                case LogLevel.Information: logger.ZLogInformation($"{tagged}"); break;
                case LogLevel.Warning: logger.ZLogWarning($"{tagged}"); break;
                case LogLevel.Error: logger.ZLogError($"{tagged}"); break;
                case LogLevel.Critical: logger.ZLogCritical($"{tagged}"); break;
            }
        }
    }
}