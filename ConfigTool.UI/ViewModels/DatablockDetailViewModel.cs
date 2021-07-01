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

namespace ConfigTool.UI.ViewModels
{
    public class DatablockDetailViewModel : ViewModelBase, IDatablockDetailViewModel
    {
        #region Fields

        private readonly IDatablockRepository _datablockRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IDatablockLookupDataService _datablockLookupDataRepository;
        private DatablockWrapper _datablock;
        private bool _hasChanges;

        #endregion

        #region Properties

        public DatablockWrapper Datablock
        {
            get { return _datablock; }
            private set
            {
                if (_datablock != value)
                { _datablock = value; }
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ObservableCollection<LookupItem<int>> Datablocks { get; }

        public bool HasChanges
        {
            get { return _hasChanges; }
            protected set
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

        public DatablockDetailViewModel(IDatablockRepository datablockRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService, IDatablockLookupDataService datablockLookupDataRepository)
        {
            _datablockRepository = datablockRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _datablockLookupDataRepository = datablockLookupDataRepository;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);

            _eventAggregator.GetEvent<ComboboxSelectionChangedEvent>().Subscribe(AfterComboboxChanged);
        }

        #endregion

        #region Methods

        private async void OnSaveExecute()
        {
            await _datablockRepository.SaveAsync();
            HasChanges = _datablockRepository.HasChanges();
            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Publish(new AfterPlctagSavedEventArgs
            {
                Id = Datablock.Id,
                DisplayMember = Datablock.Name
            });
        }

        private bool OnSaveCanExecute()
        {
            return Datablock != null && !Datablock.HasErrors && HasChanges;
        }
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the pcltag {Datablock.Id} {Datablock.Name}?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _datablockRepository.Remove(Datablock.Model);
                await _datablockRepository.SaveAsync();
                _eventAggregator.GetEvent<TagDeletedEvent>().Publish(Datablock.Id);
            }
        }

        public async Task LoadAsync(EventParameters? eventParameters)
        {
            var datablock = eventParameters != null ? await _datablockRepository.GetByIdAsync(eventParameters.Id) : CreateNewDatablock();

            InitializeDatablock(datablock);

            //await LoadDatablocksLookupAsync();
        }

        private void InitializeDatablock(DataBlock datablock)
        {
            Datablock = new DatablockWrapper(datablock);

            Datablock.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _datablockRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Datablock.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (datablock.Id == 0)
            {
                //Little trick to trigger the validation 
                datablock.Name = "";
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

        private DataBlock CreateNewDatablock()
        {
            var datablock = new DataBlock();
            _datablockRepository.Add(datablock);
            return datablock;
        }

        private async void AfterComboboxChanged(EventParameters? eventParameters)
        {
            var datablock = eventParameters != null ? await _datablockRepository.GetByIdAsync(eventParameters.Id) : CreateNewDatablock();
            //TODO moet op een of andere manier generiek gemaakt worden
            Datablock.Id = datablock.Id;
            Datablock.Name = datablock.Name;
        }
        #endregion

    }
}
