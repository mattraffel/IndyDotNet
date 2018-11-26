using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Tests
{
    /// <summary>
    /// So that all tests are correctly initialized, global test initialization 
    /// goes here.
    /// </summary>
    [TestClass]
    public class AssemblyTestInitialization
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var config = new LoggingConfiguration();
            var debuggerTarget = new DebuggerTarget("debugger")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };
            config.AddTarget(debuggerTarget);
            config.AddRuleForAllLevels(debuggerTarget);

            var consoleTarget = new ConsoleTarget("console")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);
            config.AddRuleForAllLevels(consoleTarget);

            LogManager.Configuration = config;

            IndyDotNet.Utils.Logger.Init();

            LogManager.GetCurrentClassLogger().Info("Logging started");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            LogManager.GetCurrentClassLogger().Info("Logging ending | testing ended");
        }
    }
}
