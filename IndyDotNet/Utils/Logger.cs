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
        }

        private static LogMessageDelegate LogMessageCallback = LogMessageDelegateMethod;

        public static void Init()
        {
            NativeMethods.indy_set_logger(IntPtr.Zero, null, LogMessageCallback, null);
        }
    }
}
