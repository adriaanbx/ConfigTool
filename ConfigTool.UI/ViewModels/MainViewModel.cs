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

        private readonly Func<IPlctagTableViewModel> _plctagTableViewModelCreator;
        private readonly Func<IDatablockTableViewModel> _datablockTableViewModelCreator;
        private readonly Func<IPressParameterTableViewModel> _pressParameterTableViewModelCreator;
        private readonly Func<IPressParameterTypeTableViewModel> _pressParameterTypeTableViewModelCreator;
        private readonly Func<ILayerSideTableViewModel> _LayerSideTableViewModelCreator;

        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private IDetailViewModel _DetailViewModel;
        private ITableViewModel _TableViewModel;

        public INavigationViewModel NavigationViewModel { get; }

        public ITableViewModel TableViewModel
        {
            get { return _TableViewModel; }
            private set
            {
                if (_TableViewModel != value)
                {
                    _TableViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

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
            set
            {
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
                                Func<IPlctagTableViewModel> plctagTableViewModelCreator, Func<IDatablockTableViewModel> datablockTableViewModelCreator,
                                Func<IPressParameterTableViewModel> pressParameterTableViewModelCreator, Func<IPressParameterTypeTableViewModel> pressParameterTypeTableViewModelCreator,
                                Func<ILayerSideTableViewModel> layerSideTableViewModelCreator,
                                IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _datablockDetailViewModelCreator = datablockDetailViewModelCreator;
            _valueTypeDetailViewModelCreator = valueTypeDetailViewModelCreator;
            _unitCategoryDetailViewModelCreator = unitCategoryDetailViewModelCreator;
            _textLanguageDetailViewModelCreator = textLanguageDetailViewModelCreator;
            _plctagDetailViewModelCreator = plctagDetailViewModelCreator;

            _plctagTableViewModelCreator = plctagTableViewModelCreator;
            _datablockTableViewModelCreator = datablockTableViewModelCreator;
            _pressParameterTableViewModelCreator = pressParameterTableViewModelCreator;
            _pressParameterTypeTableViewModelCreator = pressParameterTypeTableViewModelCreator;
            _LayerSideTableViewModelCreator = layerSideTableViewModelCreator;

            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenTableDetailView);
            _eventAggregator.GetEvent<TagDeletedEvent>().Subscribe(OnTagDeleted);
            _eventAggregator.GetEvent<StatusChangedEvent>().Subscribe(UpdateStatus);
            _eventAggregator.GetEvent<OpenTableViewEvent>().Subscribe(OnOpenTableView);

            NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            UpdateStatus("Loading...");
            await NavigationViewModel.LoadAsync();
            UpdateStatus("Ready");
        }

        public ICommand CreateNewPlctagCommand { get; }

        private async void OnOpenTableDetailView(EventParameters? eventParameters)
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
                case nameof(UnitCategory):
                    DetailViewModel = _unitCategoryDetailViewModelCreator();
                    break;
                case nameof(Text):
                    DetailViewModel = _textLanguageDetailViewModelCreator();
                    break;
                case nameof(Plctag):
                    DetailViewModel = _plctagDetailViewModelCreator();
                    break;
                default:
                    DetailViewModel = null;
                    return;
            }

            UpdateStatus("Loading...");
            await DetailViewModel.LoadAsync(eventParameters);
            UpdateStatus("Ready");
        }

        //TODO Wanneer alle tabellen zijn ingevoegd kan je 1 algemene functie maken van "OnOpenDetailView" en "OnOpenTableView"
        private async void OnOpenTableView(EventParameters? eventParameters)
        {
            //shows a message when you leave the detail view, if any unsaved changes have been made
            if (TableViewModel != null && TableViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            switch (eventParameters?.TableName)
            {
                case nameof(Plctag):
                case "PLCTag":
                    TableViewModel = _plctagTableViewModelCreator();
                    break;
                case nameof(DataBlock):
                    TableViewModel = _datablockTableViewModelCreator();
                    break;
                case nameof(PressParameter):
                    TableViewModel = _pressParameterTableViewModelCreator();
                    break;
                case nameof(PressParameterType):
                    TableViewModel = _pressParameterTypeTableViewModelCreator();
                    break;
                case nameof(LayerSide):
                case ("LAYER_SIDE"):
                    TableViewModel = _LayerSideTableViewModelCreator();
                    break;
                default:
                    //CleanTableView(); -> zorgt voor crash
                    CleanDetailView();
                    return;
            }

            UpdateStatus("Loading...");
            await TableViewModel.LoadAsync(eventParameters);
            CleanDetailView();
            UpdateStatus("Ready");
        }

        private void UpdateStatus(string message)
        {
            Status = message;
        }

        private void OnTagDeleted(int tag)
        {
            CleanDetailView();
        }

        private void CleanDetailView()
        {
            DetailViewModel = null;
        }
        private void CleanTableView()
        {
            TableViewModel = null;
        }




    }
}
