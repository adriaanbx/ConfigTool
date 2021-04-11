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
        private readonly Func<IDatablockDetailViewModel> _datablockDetailViewModelCreator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private IDatablockDetailViewModel _datablockDetailViewModel;

        public INavigationViewModel NavigationViewModel { get; }

        public IDatablockDetailViewModel DatablockDetailViewModel
        {
            get { return _datablockDetailViewModel; }
            private set
            {
                if (_datablockDetailViewModel != value)
                {
                    _datablockDetailViewModel = value;
                    OnPropertyChanged();
                }
            }
        }


        public MainViewModel(INavigationViewModel navigationViewModel, Func<IDatablockDetailViewModel> plctagDetailViewModelCreator, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _datablockDetailViewModelCreator = plctagDetailViewModelCreator;

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

        private async void OnOpenPlctagDetailView(EventParameters? eventParameters)
        {
            if (DatablockDetailViewModel != null && DatablockDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            DatablockDetailViewModel = _datablockDetailViewModelCreator();
            await DatablockDetailViewModel.LoadAsync(eventParameters);
        }
        private void OnCreatenewPlctagExecute()
        {
            OnOpenPlctagDetailView(null);
        }

        private void AfterPlctagDeleted(int plctagId)
        {
            DatablockDetailViewModel = null;
        }

    }
}
