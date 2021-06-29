using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Views.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConfigTool.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Func<IDatablockDetailViewModel> _datablockDetailViewModelCreator;
        private readonly Func<IValueTypeDetailViewModel> _valueTypeDetailViewModelCreator;
        private readonly Func<IUnitCategoryDetailViewModel> _unitCategoryDetailViewModelCreator;
        private readonly Func<ITextLanguageDetailViewModel> _textLanguageDetailViewModelCreator;
        private readonly Func<IPlctagDetailViewModel> _plctagDetailViewModelCreator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private IDetailViewModel _DetailViewModel;

        public INavigationViewModel NavigationViewModel { get; }

        public IDetailViewModel DetailViewModel
        {
            get { return _DetailViewModel; }
            private set
            {
                if (_DetailViewModel != value)
                {
                    _DetailViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }



        public MainViewModel(INavigationViewModel navigationViewModel, Func<IDatablockDetailViewModel> datablockDetailViewModelCreator,
                                Func<IValueTypeDetailViewModel> valueTypeDetailViewModelCreator, Func<IUnitCategoryDetailViewModel> unitCategoryDetailViewModelCreator,
                                Func<ITextLanguageDetailViewModel> textLanguageDetailViewModelCreator, Func<IPlctagDetailViewModel> plctagDetailViewModelCreator,
                                IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _datablockDetailViewModelCreator = datablockDetailViewModelCreator;
            _valueTypeDetailViewModelCreator = valueTypeDetailViewModelCreator;
            _unitCategoryDetailViewModelCreator = unitCategoryDetailViewModelCreator;
            _textLanguageDetailViewModelCreator = textLanguageDetailViewModelCreator;
            _plctagDetailViewModelCreator = plctagDetailViewModelCreator;

            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<OpenPlctagDetailViewEvent>().Subscribe(OnOpenPlctagDetailView);
            _eventAggregator.GetEvent<AfterPlctagDeletedEvent>().Subscribe(AfterPlctagDeleted);
            _eventAggregator.GetEvent<StatusChangedEvent>().Subscribe(UpdateStatus);
           
            NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            UpdateStatus("Loading...");
            await NavigationViewModel.LoadAsync();
            UpdateStatus("Ready");
        }

        public ICommand CreateNewPlctagCommand { get; }

        private async void OnOpenPlctagDetailView(EventParameters? eventParameters)
        {
            //shows a message when you leave the detail view, if any unsaved changes have been made
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            switch (eventParameters?.TableName)
            {
                case nameof(DataBlock):
                    DetailViewModel = _datablockDetailViewModelCreator();
                    break;
                case nameof(Models.ValueType):
                    DetailViewModel = _valueTypeDetailViewModelCreator();
                    break;
                case nameof(Models.UnitCategory):
                    DetailViewModel = _unitCategoryDetailViewModelCreator();
                    break;
                case nameof(Models.Text):
                    DetailViewModel = _textLanguageDetailViewModelCreator();
                    break;
                default:
                    DetailViewModel = _plctagDetailViewModelCreator();
                    break;
            }

            UpdateStatus("Loading...");
            await DetailViewModel.LoadAsync(eventParameters);
            UpdateStatus("Ready");
        }

        private void UpdateStatus(string message)
        {
            Status = message;
        }

        private void AfterPlctagDeleted(int plctagId)
        {
            DetailViewModel = null;
        }

    }
}
