﻿using System;
using Terminal.Gui;
using IndyDotNet.Pool;

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
                EditDialog dlg = new EditDialog("Create Pool", "Enter pool name:");
                var dlgResults = dlg.Show();
                if (DialogConstants.OK == dlgResults.Item1)
                {
                    IPool pool = Factory.GetPool($"pool_{dlgResults.Item2}");
                    pool.Create();
                }

            }
            catch(Exception ex)
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
                }

            }
            catch (Exception ex)
            {
                MessageBox.Query(70, 7, "Pool Create Failed", $"Error: {ex.Message}", "Ok");
            }
        }

        private void ClosePool()
        {
        }

    }
}
