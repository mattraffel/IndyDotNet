using System;
using Terminal.Gui;
using Hyperledger.Indy.PoolApi;


namespace IndyDotNetCLI.Views
{
    public class PoolHandler
    {
        public PoolHandler()
        { }

        public MenuBarItem CreateMenu()
        {
            return new MenuBarItem("_Pool", new MenuItem[] {
                new MenuItem ("_Create", "", () => { CreatePool(); }),
                new MenuItem ("_Open", "", () => { OpenPool(); }),
                new MenuItem ("_Close", "", () => { ClosePool(); })
            });
        }

        private void CreatePool() 
        {
            try
            {
                Pool.CreatePoolLedgerConfigAsync("nameTBD", null).Wait(5000);
            }
            catch (Exception ex)
            {
                MessageBox.Query(70, 7, "Pool Create Failed", $"Error: {ex.Message}", "Ok");
            }
        }

        private void OpenPool() 
        {

        }

        private void ClosePool()
        {
            MessageBox.Query(50, 7, "Menu Selected", "Close", "Yes");
        }

    }
}
