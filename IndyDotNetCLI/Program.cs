using System;
using Terminal.Gui;

using IndyDotNetCLI.Views;


namespace IndyDotNetCLI
{
    class Program
    {
        private static Window _mainWindow = null;
        private static Label _poolStatus = new Label(3, 2, "Pool: not open");
        private static Label _walletStatus = new Label(3, 4, "Wallet: not open");
        private static Label _didStatus = new Label(3, 6, "Did: none");

        private static PoolHandler _poolHanlder = new PoolHandler();

        static bool ConfirmQuit()
        {
            var n = MessageBox.Query(50, 7, "Quit IndyDotNetCLI", "Are you sure you want to quit?", "Yes", "No");
            return n == 0;
        }

        static void InitializeLogging()
        {
            // all logging currently goes to Debug.Console.
            // for mac os: View -> Pads -> Application Output
            IndyDotNet.Utils.Logger.Init();
        }

        static void Main(string[] args)
        {
            InitializeLogging();

            Application.Init();
            var top = Application.Top;

            _mainWindow = new Window(new Rect(0, 1, top.Frame.Width, top.Frame.Height - 1), "IndyDotNet Command Line");
            top.Add(_mainWindow);

            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_Application", new MenuItem [] {
                        new MenuItem ("_Quit", "", () => { if (ConfirmQuit ()) top.Running = false; })
                    }),
                _poolHanlder.CreateMenu()
            });
            top.Add(menu);

            // Add some controls
            _mainWindow.Add(
                _poolStatus,
                _walletStatus,
                _didStatus,
                new Label(3, top.Frame.Height - 5, "Press ESC and 9 to activate the menubar"),
                new Label(3, top.Frame.Height - 6, System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
            );

            Application.Run();
        }
    }
}
