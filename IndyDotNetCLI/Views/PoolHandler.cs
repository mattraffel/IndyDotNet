using System;
using Terminal.Gui;
using IndyDotNet.Pool;
using System.Collections.Generic;

namespace IndyDotNetCLI.Views
{
    public class PoolHandler
    {
        #region public methods
        public PoolHandler()
        { }

        public MenuBarItem CreateMenu()
        {
            return new MenuBarItem("_Pool", new MenuItem[] {
                new MenuItem("_Create", "", () => { CreatePool(); }),
                new MenuItem("_Open", "", () => { OpenPool(); }),
                new MenuItem("_Close", "", () => { ClosePool(); }),
                new MenuItem("_List", "", () => { ListPools();})
            });
        }
        #endregion

        #region private data
        private List<IPool> _openPools = new List<IPool>();
        #endregion

        #region private methods
        private void CreatePool()
        {
            try
            {
                EditDialog dlg = new EditDialog("Create Pool", "Enter pool name:");
                var dlgResults = dlg.Show();
                if (DialogConstants.OK == dlgResults.Item1)
                {
                    IPool pool = Factory.GetPool($"pool_{dlgResults.Item2}");
                    pool.Create();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Query(70, 7, "Pool Create Failed", $"Error: {ex.Message}", "Ok");
            }
        }

        private void OpenPool()
        {
            try
            {
                EditDialog dlg = new EditDialog("Open Pool", "Enter pool name:");
                var dlgResults = dlg.Show();
                if (DialogConstants.OK == dlgResults.Item1)
                {
                    IPool pool = Factory.GetPool($"pool_{dlgResults.Item2}");
                    pool.Open();
                    _openPools.Add(pool);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Query(70, 7, "Pool Open Failed", $"Error: {ex.Message}", "Ok");
            }
        }

        private void ClosePool()
        {
            try 
            {

            }
            catch (Exception ex)
            {
                MessageBox.Query(70, 7, "Pool Close Failed", $"Error: {ex.Message}", "Ok");
            }
        }

        private void ListPools()
        {
            try
            {
                List<IPool> pools = Factory.ListPools();
                MessageBox.Query(70, 7, "List pools", $"{pools[0].Name}", "Ok");
            }
            catch (Exception ex)
            {
                MessageBox.Query(70, 7, "List Pools Failed", $"Error: {ex.Message}", "Ok");
            }
        }
        #endregion

    }
}
