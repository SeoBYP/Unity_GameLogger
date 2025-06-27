using System;
using System.IO;
using Microsoft.Extensions.Logging;
using UnityEngine;
using ZLogger;
using ZLogger.Providers;
using ZLogger.Unity;

namespace Core.Logging
{
    public static class LogInitializer
    {
        public static ILoggerFactory LoggerFactory { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            // 로그 파일 저장 경로
            var logDir = Path.Combine(Application.persistentDataPath, "logs");
            Directory.CreateDirectory(logDir);

            LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace); // 최소 로그 레벨 설정

                // Unity 콘솔 로그 출력
                builder.AddZLoggerUnityDebug();

                // Rolling File 로그 출력 설정
                builder.AddZLoggerRollingFile(options =>
                {
                    options.FilePathSelector = (ts, index) =>
                        Path.Combine(logDir, $"log_{ts:yyyy-MM-dd}_{index}.log");

                    options.RollingInterval = RollingInterval.Day;
                    options.RollingSizeKB = 1024;

                    // 텍스트 기반 포맷터 설정 (Prefix + 줄바꿈 포함)
                    options.UsePlainTextFormatter(formatter =>
                    {
                        formatter.SetPrefixFormatter(
                            $"{0:local-timeonly} [{1:short}] ",
                            (in MessageTemplate tpl, in LogInfo info) => tpl.Format(info.Timestamp, info.LogLevel)
                        );

                        // ✅ 개행 문자 강제로 추가
                        formatter.SetSuffixFormatter(
                            $"\\n",
                            (in MessageTemplate tpl, in LogInfo info) => tpl.Format()
                        );
                    });
                });
            });

            GameLog.Initialize(LoggerFactory); // 전역 로그 초기화

            var logger = LoggerFactory.CreateLogger("LogInitializer");
            logger.ZLogInformation($"✅ ZLogger Initialized! Logs will be saved to {logDir}");

            // 애플리케이션 종료 시 로그 flush
            Application.exitCancellationToken.Register(() => LoggerFactory.Dispose());
        }
    }
}
