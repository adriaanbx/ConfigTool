using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Func<IPlctagDetailViewModel> _plctagDetailViewModelCreator;
        private readonly IEventAggregator _eventAggregator;
        private IPlctagDetailViewModel _plctagDetailViewModel;

        public INavigationViewModel NavigationViewModel { get; }

        public IPlctagDetailViewModel PlctagDetailViewModel
        {
            get { return _plctagDetailViewModel; }
            private set
            {
                if (_plctagDetailViewModel != value)
                {
                    _plctagDetailViewModel = value;
                    OnPropertyChanged();
                }
            }
        }


        public MainViewModel(INavigationViewModel navigationViewModel, Func<IPlctagDetailViewModel> plctagDetailViewModelCreator, IEventAggregator eventAggregator)
        {
            _plctagDetailViewModelCreator = plctagDetailViewModelCreator;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenPlctagDetailViewEvent>()
                .Subscribe(OnOpenPlctagDetailView);

            NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenPlctagDetailView(int plctagId)
        {
            PlctagDetailViewModel = _plctagDetailViewModelCreator();
            await PlctagDetailViewModel.LoadAsync(plctagId);
        }
    }
}
