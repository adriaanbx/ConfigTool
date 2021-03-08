using ConfigTool.UI.ViewModel;
using System;
using System.Windows;

namespace ConfigTool.UI
{
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel;


        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _mainViewModel = mainViewModel;
            DataContext = _mainViewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           await _mainViewModel.LoadAsync();
        }
    }
}
