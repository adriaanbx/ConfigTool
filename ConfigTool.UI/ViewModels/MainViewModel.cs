using ConfigTool.UI.Events;
using ConfigTool.UI.Views.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Func<IPlctagDetailViewModel> _plctagDetailViewModelCreator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
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


        public MainViewModel(INavigationViewModel navigationViewModel, Func<IPlctagDetailViewModel> plctagDetailViewModelCreator, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _plctagDetailViewModelCreator = plctagDetailViewModelCreator;

            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
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
            if (PlctagDetailViewModel != null && PlctagDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            PlctagDetailViewModel = _plctagDetailViewModelCreator();
            await PlctagDetailViewModel.LoadAsync(plctagId);
        }
    }
}
