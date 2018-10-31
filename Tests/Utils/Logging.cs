using System;
using NLog;

namespace Tests.Utils
{
    /// <summary>
    /// Facade to IndyDotNet Logging/NLog in case that changes.
    /// </summary>
    public static class Logging
    {
        public static void Error(string error)
        {
            LogManager.GetCurrentClassLogger().Error(error);
        }

        public static void Info(string info)
        {
            LogManager.GetCurrentClassLogger().Info(info);
        }
    }
}
