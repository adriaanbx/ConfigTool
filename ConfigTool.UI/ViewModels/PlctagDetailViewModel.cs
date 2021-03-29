using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Lookups;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConfigTool.UI.ViewModel
{
    public class PlctagDetailViewModel : ViewModelBase, IPlctagDetailViewModel
    {
        #region Fields

        private readonly IPlctagRepository _plctagRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IDatablockLookupDataService _datablockLookupDataRepository;
        private PlctagWrapper _plctag;
        private bool _hasChanges;
        #endregion

        #region Properties

        public PlctagWrapper Plctag
        {
            get { return _plctag; }
            private set
            {
                if (_plctag != value)
                { _plctag = value; }
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ObservableCollection<LookupItem> Datablocks { get; }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

            }
        }
        #endregion

        #region Constructors

        public PlctagDetailViewModel(IPlctagRepository plctagRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService, IDatablockLookupDataService datablockLookupDataRepository)
        {
            _plctagRepository = plctagRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _datablockLookupDataRepository = datablockLookupDataRepository;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);

            Datablocks = new ObservableCollection<LookupItem>();
        }

        #endregion

        #region Methods

        private async void OnSaveExecute()
        {
            await _plctagRepository.SaveAsync();
            HasChanges = _plctagRepository.HasChanges();
            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Publish(new AfterPlctagSavedEventArgs
            {
                Id = Plctag.Id,
                DisplayMember = Plctag.Name
            });
        }

        private bool OnSaveCanExecute()
        {
            return Plctag != null && !Plctag.HasErrors && HasChanges;
        }
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the pcltag {Plctag.Id} {Plctag.Name}?","Question");
            if (result== MessageDialogResult.OK)
            {
                _plctagRepository.Remove(Plctag.Model);
                await _plctagRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterPlctagDeletedEvent>().Publish(Plctag.Id);
            }            
        }

        public async Task LoadAsync(int? plctagId)
        {
            var plctag = plctagId.HasValue ? await _plctagRepository.GetByIdAsync(plctagId.Value) : CreateNewPlctag();
            
            InitializePlctag(plctag);

            await LoadDatablocksLookupAsync();
        }

        private void InitializePlctag(Plctag plctag)
        {
            Plctag = new PlctagWrapper(plctag);

            Plctag.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _plctagRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Plctag.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (plctag.Id == 0)
            {
                //Little trick to trigger the validation 
                plctag.Name = "";
            }
        }

        private async Task LoadDatablocksLookupAsync()
        {
            Datablocks.Clear();
            var lookup = await _datablockLookupDataRepository.GetDatablockLookupAsync();
            foreach (var lookupItem in lookup)
            {
                Datablocks.Add(lookupItem);
            }
        }

        private Plctag CreateNewPlctag()
        {
            var plctag = new Plctag();
            _plctagRepository.Add(plctag);
            return plctag;
        }
        #endregion

    }
}
