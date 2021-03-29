using ConfigTool.UI.Events;
using ConfigTool.UI.Views.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

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
            _eventAggregator.GetEvent<OpenPlctagDetailViewEvent>().Subscribe(OnOpenPlctagDetailView);
            _eventAggregator.GetEvent<AfterPlctagDeletedEvent>().Subscribe(AfterPlctagDeleted);

            CreateNewPlctagCommand = new DelegateCommand(OnCreatenewPlctagExecute);

            NavigationViewModel = navigationViewModel;
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public ICommand CreateNewPlctagCommand { get; }

        private async void OnOpenPlctagDetailView(int? plctagId)
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
        private void OnCreatenewPlctagExecute()
        {
            OnOpenPlctagDetailView(null);
        }

        private void AfterPlctagDeleted(int plctagId)
        {
            PlctagDetailViewModel = null;
        }

    }
}
