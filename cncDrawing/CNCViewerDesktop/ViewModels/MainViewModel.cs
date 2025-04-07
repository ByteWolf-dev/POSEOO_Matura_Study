using Base.WpfMvvm;
using CNCViewerDesktop.Controls;
using Core;
using Core.Entities;
using Microsoft.Win32;

namespace CNCViewerDesktop.ViewModels
{    
    public class MainViewModel : BaseViewModel
    {
       
        public MainViewModel()
        {
            DrawingControl = new DrawingControl();
        }

        public DrawingControl DrawingControl { get; set; }

        private async Task ImportGCodeFileAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GCode file (*.nc)|*.nc";
            if (openFileDialog.ShowDialog() == true)
            {
                var filename = openFileDialog.FileName;
                await Task.CompletedTask;

                DrawingControl.Pattern = await GCodeParser.ParsePatternFromGcodeAsync(filename);
                await Console.Out.WriteLineAsync();
               
                // TODO: Load GCode file
            }
        }

        public async override Task InitializeDataAsync()
        {
            await ImportGCodeFileAsync();
            await Task.CompletedTask;
        }
    }
}
