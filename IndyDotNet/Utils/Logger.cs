using System;
using System.Diagnostics;

using static IndyDotNet.Utils.NativeMethods;

#if __IOS__
using ObjCRuntime;
#endif
namespace IndyDotNet.Utils
{
    public class Logger
    {

#if __IOS__
        [MonoPInvokeCallback(typeof(LogMessageDelegate))]
#endif
        private static void LogMessageDelegateMethod(IntPtr context, int level, string target, string message, string module_path, string file, int line)
        {
            // TODO:  need to filter by log level etc
            Debug.WriteLine("Level {0} Target {1} Message {2}", level, target, message);

            var logger = NLog.LogManager.GetCurrentClassLogger();
            var logMessage = string.Format("{0}:{1} | {2}", file, line, message);

            switch (level)
            {
                case 1:
                    logger.Error(logMessage);
                    break;
                case 2:
                    logger.Warn(logMessage);
                    break;
                case 3:
                    logger.Info(logMessage);
                    break;
                case 4:
                    logger.Debug(logMessage);
                    break;
                case 5:
                    logger.Trace(logMessage);
                    break;
                default:
                    break;
            }
        }

        private static LogMessageDelegate LogMessageCallback = LogMessageDelegateMethod;

        public static void Init()
        {
            NativeMethods.indy_set_logger(IntPtr.Zero, null, LogMessageCallback, null);
        }
    }
}
