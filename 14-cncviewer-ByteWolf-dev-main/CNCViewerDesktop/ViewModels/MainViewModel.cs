using Base.WpfMvvm;

using Core;
using Core.Entities;
using Microsoft.Win32;

namespace CNCViewerDesktop.ViewModels
{    
    public class MainViewModel : BaseViewModel
    {
       
        public MainViewModel()
        {

        }

        private async Task ImportGCodeFileAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GCode file (*.nc)|*.nc";
            if (openFileDialog.ShowDialog() == true)
            {
                var filename = openFileDialog.FileName;
                await Task.CompletedTask;
                // TODO: Load GCode file
            }
        }

        public async override Task InitializeDataAsync()
        {
            await Task.CompletedTask;
        }
    }
}
