using CNCViewerDesktop.ViewModels;

using System.Windows;

namespace CNCViewerDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            Loaded += async (v, e) =>
            {
                await (DataContext as MainViewModel)!
                    .InitializeDataAsync();
            };
        }
    }
}