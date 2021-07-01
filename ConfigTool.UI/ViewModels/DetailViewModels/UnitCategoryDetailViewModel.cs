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
    public class UnitCategoryDetailViewModel : ViewModelBase, IUnitCategoryDetailViewModel
    {
        #region Fields

        private readonly IUnitCategoryRepository _unitCategoryRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IUnitCategoryLookupDataService _unitCategoryLookupDataRepository;
        private UnitCategoryWrapper _unitCategory;
        private bool _hasChanges;
        #endregion

        #region Properties

        public UnitCategoryWrapper UnitCategory
        {
            get { return _unitCategory; }
            private set
            {
                if (_unitCategory != value)
                { _unitCategory = value; }
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ObservableCollection<LookupItem<int>> UnitCategories { get; }

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

        public UnitCategoryDetailViewModel(IUnitCategoryRepository unitCategoryRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService, IUnitCategoryLookupDataService unitCategoryLookupDataRepository)
        {
            _unitCategoryRepository = unitCategoryRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _unitCategoryLookupDataRepository = unitCategoryLookupDataRepository;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);

            
        }

        #endregion

        #region Methods

        private async void OnSaveExecute()
        {
            await _unitCategoryRepository.SaveAsync();
            HasChanges = _unitCategoryRepository.HasChanges();
            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Publish(new AfterPlctagSavedEventArgs
            {
                Id = UnitCategory.Id,
                DisplayMember = UnitCategory.Name
            });
        }

        private bool OnSaveCanExecute()
        {
            return UnitCategory != null && !UnitCategory.HasErrors && HasChanges;
        }
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the pcltag {UnitCategory.Id} {UnitCategory.Name}?","Question");
            if (result== MessageDialogResult.OK)
            {
                _unitCategoryRepository.Remove(UnitCategory.Model);
                await _unitCategoryRepository.SaveAsync();
                _eventAggregator.GetEvent<TagDeletedEvent>().Publish(UnitCategory.Id);
            }            
        }

        public async Task LoadAsync(EventParameters? eventParameters)
        {
            var valueType = eventParameters!=null ? await _unitCategoryRepository.GetByIdAsync(eventParameters.Id) : CreateNewUnitCategory();

            InitializeUnitCategories(valueType);

        }

        private void InitializeUnitCategories(UnitCategory valueType)
        {
            UnitCategory = new UnitCategoryWrapper(valueType);

            UnitCategory.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _unitCategoryRepository.HasChanges();
                }

                if (e.PropertyName == nameof(UnitCategory.HasErrors))
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


        private UnitCategory CreateNewUnitCategory()
        {
            var valueType = new UnitCategory();
            _unitCategoryRepository.Add(valueType);
            return valueType;
        }
        #endregion

    }
}
