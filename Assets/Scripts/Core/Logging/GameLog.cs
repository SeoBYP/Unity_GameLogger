using System;
using System.Text;
using Microsoft.Extensions.Logging;
using UnityEngine;
using ZLogger;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Core.Logging
{
    public static class GameLog
    {
        private static ILogger _logger;

        public static void Initialize(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger("Global");
        }

        public static void Log(string msg) => _logger.ZLogInformation($"{msg}");
        public static void LogWarning(string msg) => _logger.ZLogWarning($"{msg}");
        public static void LogError(string msg) => _logger.ZLogError($"{msg}");

        public static void Exception(Exception ex, string? ctx = null) =>
            _logger.ZLogError(ex, $"{ctx ?? ex.Message}");
        
        // ✅ StringBuilder 기반 체이닝 로그 빌더
        public static GameLogBuilder Builder() => new(new StringBuilder());
    }

    public static class GameLogExtension
    {
        /// <summary>
        /// 조건이 false일 경우 로그 출력 + 에디터 Assert
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                GameLog.LogError($"[ASSERT] {message}");
                Debug.Assert(false, message);
            }
        }

        /// <summary>
        /// 조건이 false일 경우 로그 출력 + 예외 throw
        /// </summary>
        public static void AssertThrow(bool condition, string message)
        {
            if (!condition)
            {
                GameLog.LogError($"[EXCEPTION] {message}");
                throw new System.Exception(message);
            }
        }

        /// <summary>
        /// 개발자 전용 로그 (에디터에서만 출력)
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void DevOnlyLog(bool condition, string message)
        {
            if (condition)
            {
                GameLog.Log($"[DEV] {message}");
            }
        }

        /// <summary>
        /// 조건이 true일 경우 경고 출력
        /// </summary>
        public static void WarnIf(bool condition, string message)
        {
            if (condition)
            {
                GameLog.LogWarning($"[WarnIf] {message}");
            }
        }
    }
}