using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public INavigationViewModel NavigationViewModel { get; }
        public IPlctagDetailViewModel PlctagDetailViewModel { get; }

        public MainViewModel(INavigationViewModel navigationViewModel, IPlctagDetailViewModel plctagDetailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            PlctagDetailViewModel = plctagDetailViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
    }
}
