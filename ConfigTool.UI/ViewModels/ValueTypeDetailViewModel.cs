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
using ValueType = ConfigTool.Models.ValueType;

namespace ConfigTool.UI.ViewModels
{
    public class ValueTypeDetailViewModel : ViewModelBase, IValueTypeDetailViewModel
    {
        #region Fields

        private readonly IValueTypeRepository _valueTypeRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IValueTypeLookupDataService _valueTypeLookupDataRepository;
        private ValueTypeWrapper _valueType;
        private bool _hasChanges;
        #endregion

        #region Properties

        public ValueTypeWrapper ValueType
        {
            get { return _valueType; }
            private set
            {
                if (_valueType != value)
                { _valueType = value; }
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ObservableCollection<LookupItem<short>> ValueTypes { get; }

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

        public ValueTypeDetailViewModel(IValueTypeRepository valueTypeRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService, IValueTypeLookupDataService valueTypeLookupDataRepository)
        {
            _valueTypeRepository = valueTypeRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _valueTypeLookupDataRepository = valueTypeLookupDataRepository;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);

            //ValueTypes = new ObservableCollection<LookupItem>();
        }

        #endregion

        #region Methods

        private async void OnSaveExecute()
        {
            await _valueTypeRepository.SaveAsync();
            HasChanges = _valueTypeRepository.HasChanges();
            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Publish(new AfterPlctagSavedEventArgs
            {
                Id = ValueType.Id,
                DisplayMember = ValueType.Name
            });
        }

        private bool OnSaveCanExecute()
        {
            return ValueType != null && !ValueType.HasErrors && HasChanges;
        }
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the pcltag {ValueType.Id} {ValueType.Name}?","Question");
            if (result== MessageDialogResult.OK)
            {
                _valueTypeRepository.Remove(ValueType.Model);
                await _valueTypeRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterPlctagDeletedEvent>().Publish(ValueType.Id);
            }            
        }

        public async Task LoadAsync(EventParameters? eventParameters)
        {
            var valueType = eventParameters!=null ? await _valueTypeRepository.GetByIdAsync(eventParameters.Id) : CreateNewValueType();

            InitializeValueTypes(valueType);

        }

        private void InitializeValueTypes(ValueType valueType)
        {
            ValueType = new ValueTypeWrapper(valueType);

            ValueType.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _valueTypeRepository.HasChanges();
                }

                if (e.PropertyName == nameof(ValueType.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (valueType.Id == 0)
            {
                //Little trick to trigger the validation 
                valueType.Name = "";
            }
        }


        private ValueType CreateNewValueType()
        {
            var valueType = new ValueType();
            _valueTypeRepository.Add(valueType);
            return valueType;
        }
        #endregion

    }
}
