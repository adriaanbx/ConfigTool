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
        private readonly Func<IPressParameterTypeDetailViewModel> _pressParameterTypeDetailViewModelCreator;
        private readonly Func<ILayerSideDetailViewModel> _layerSideDetailViewModelCreator;
        private readonly Func<IReadWriteTypeDetailViewModel> _readWriteTypeDetailViewModelCreator;

        private readonly Func<IPlctagTableViewModel> _plctagTableViewModelCreator;
        private readonly Func<IDatablockTableViewModel> _datablockTableViewModelCreator;
        private readonly Func<IPressParameterTableViewModel> _pressParameterTableViewModelCreator;
        private readonly Func<IPressParameterTypeTableViewModel> _pressParameterTypeTableViewModelCreator;
        private readonly Func<ILayerSideTableViewModel> _layerSideTableViewModelCreator;
        private readonly Func<IEngineeringTableViewModel> _engineeringTableViewModelCreator;
        private readonly Func<IReadWriteTypeTableViewModel> _readWriteTypeTableViewModelCreator;
        private readonly Func<IEquipmentTableViewModel> _equipmentTableViewModelCreator;
        private readonly Func<IEcmParameterTableViewModel> _ecmParameterTableViewModelCreator;

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
                                Func<IPressParameterTypeDetailViewModel> pressParameterTypeDetailViewModelCreator, Func<ILayerSideDetailViewModel> layerSideDetailViewModelCreator, Func<IReadWriteTypeDetailViewModel> readWriteTypeDetailViewModelCreator,
                                Func<IPlctagTableViewModel> plctagTableViewModelCreator, Func<IDatablockTableViewModel> datablockTableViewModelCreator,
                                Func<IPressParameterTableViewModel> pressParameterTableViewModelCreator, Func<IPressParameterTypeTableViewModel> pressParameterTypeTableViewModelCreator,
                                Func<ILayerSideTableViewModel> layerSideTableViewModelCreator, Func<IEngineeringTableViewModel> engineeringTableViewModelCreator, Func<IReadWriteTypeTableViewModel> readWriteTypeTableViewModelCreator,
                                Func<IEquipmentTableViewModel> equipmentTableViewModelCreator, Func<IEcmParameterTableViewModel> ecmParameterTableViewModelCreator,
                                IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _datablockDetailViewModelCreator = datablockDetailViewModelCreator;
            _valueTypeDetailViewModelCreator = valueTypeDetailViewModelCreator;
            _unitCategoryDetailViewModelCreator = unitCategoryDetailViewModelCreator;
            _textLanguageDetailViewModelCreator = textLanguageDetailViewModelCreator;
            _plctagDetailViewModelCreator = plctagDetailViewModelCreator;
            _pressParameterTypeDetailViewModelCreator = pressParameterTypeDetailViewModelCreator;
            _layerSideDetailViewModelCreator = layerSideDetailViewModelCreator;
            _readWriteTypeDetailViewModelCreator = readWriteTypeDetailViewModelCreator;

            _plctagTableViewModelCreator = plctagTableViewModelCreator;
            _datablockTableViewModelCreator = datablockTableViewModelCreator;
            _pressParameterTableViewModelCreator = pressParameterTableViewModelCreator;
            _pressParameterTypeTableViewModelCreator = pressParameterTypeTableViewModelCreator;
            _layerSideTableViewModelCreator = layerSideTableViewModelCreator;
            _engineeringTableViewModelCreator = engineeringTableViewModelCreator;
            _equipmentTableViewModelCreator = equipmentTableViewModelCreator;
            _readWriteTypeTableViewModelCreator = readWriteTypeTableViewModelCreator;
            _ecmParameterTableViewModelCreator = ecmParameterTableViewModelCreator;

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
                case nameof(LayerSide):
                case ("LAYER_SIDE"):
                    DetailViewModel = _layerSideDetailViewModelCreator();
                    break;
                case nameof(Plctag):
                case "Tag":
                    DetailViewModel = _plctagDetailViewModelCreator();
                    break;
                case nameof(PressParameterType):
                    DetailViewModel = _pressParameterTypeDetailViewModelCreator();
                    break;
                case nameof(ReadWriteType):
                    DetailViewModel = _readWriteTypeDetailViewModelCreator();
                    break;
                case nameof(Text):
                case "GroupText":
                    DetailViewModel = _textLanguageDetailViewModelCreator();
                    break;
                case nameof(UnitCategory):
                    DetailViewModel = _unitCategoryDetailViewModelCreator();
                    break;
                case nameof(Models.ValueType):
                    DetailViewModel = _valueTypeDetailViewModelCreator();
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
                case nameof(DataBlock):
                    TableViewModel = _datablockTableViewModelCreator();
                    break;
                case nameof(Ecmparameter):
                case "ECMParameter":
                    TableViewModel = _ecmParameterTableViewModelCreator();
                    break;
                case nameof(Engineering):
                    TableViewModel = _engineeringTableViewModelCreator();
                    break;
                case nameof(Equipment):
                    TableViewModel = _equipmentTableViewModelCreator();
                    break;
                case nameof(Plctag):
                case "PLCTag":
                    TableViewModel = _plctagTableViewModelCreator();
                    break;
                case nameof(PressParameter):
                    TableViewModel = _pressParameterTableViewModelCreator();
                    break;
                case nameof(PressParameterType):
                    TableViewModel = _pressParameterTypeTableViewModelCreator();
                    break;
                case nameof(LayerSide):
                case ("LAYER_SIDE"):
                    TableViewModel = _layerSideTableViewModelCreator();
                    break;
                case nameof(ReadWriteType):
                    TableViewModel = _readWriteTypeTableViewModelCreator();
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
