using System;
using NLog;

namespace SqlServerDockerCSharp
{
    public class InjectLog
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void InjectLogInfo(String text)
        {
            logger.Info(text);

        }
        public static void InjectLogDebug(String text)
        {
            logger.Debug(text);

        }
        public static void InjectLogError(String text)
        {
            logger.Error(text);

        }
    }
}
