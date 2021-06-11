using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ConfigTool.UI.Lookups;
using System.Windows.Input;
using Prism.Commands;

namespace ConfigTool.UI.ViewModels
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IPlctagLookupDataService _plctagLookupDataRepository;
        private readonly IValueTypeLookupDataService _valueTypeLookupDataRepository;
        private readonly IDatablockLookupDataService _datablockLookupDataRepository;
        private readonly IUnitCategoryLookupDataService _unitCategoryLookupDataRepository;
        private readonly IEventAggregator _eventAggregator;
        private bool _hasChanges;

        public ObservableCollection<NavigationItemPlctag> Plctags { get; }
        public ObservableCollection<LookupItem<short>> ValueTypes { get; }
        public ObservableCollection<LookupItem<int>> Datablocks { get; }
        public ObservableCollection<LookupItem<int>> UnitCategories { get; }

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
                    ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                }

            }
        }

        private NavigationItemPlctag _selectedCell;

        public NavigationItemPlctag SelectedCell
        {
            get { return _selectedCell; }
            set
            {
                if (value.ColumnName != null && _selectedCell != value)
                {
                    _selectedCell = value;

                    //get selected column name
                    var columnName = SelectedCell.ColumnName;

                    //get foreign keys
                    var foreignKeys = _plctagLookupDataRepository.GetForeignKeys();

                    //Open details view when selected column is a foreign key
                    foreach (var key in foreignKeys)
                    {
                        if (key.PrincipalEntityType.ToString().Contains(columnName))
                        {
                            //get primarykey column name of foreignkey table
                            var primaryKeyColumnName = columnName + "Id";

                            //use reflection to get the value of the property, aka selected column, at runtime
                            var primaryKeyValue = SelectedCell.Plctag.GetType().GetProperty(primaryKeyColumnName)?.GetValue(SelectedCell.Plctag);

                            //Publish event to subscribers
                            _eventAggregator.GetEvent<OpenPlctagDetailViewEvent>()
                                .Publish(new EventParameters() { Id = Convert.ToInt32(primaryKeyValue), TableName = columnName });
                        }
                    }
                    OnPropertyChanged();

                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }


        public NavigationViewModel(IPlctagLookupDataService plctagLookupDataService, IValueTypeLookupDataService valueTypeLookupDataService, IDatablockLookupDataService datablockLookupDataService, IUnitCategoryLookupDataService unitCategoryLookupDataService, IEventAggregator eventAggregator)
        {
            _plctagLookupDataRepository = plctagLookupDataService;
            _valueTypeLookupDataRepository = valueTypeLookupDataService;
            _datablockLookupDataRepository = datablockLookupDataService;
            _unitCategoryLookupDataRepository = unitCategoryLookupDataService;
            _eventAggregator = eventAggregator;

            Plctags = new ObservableCollection<NavigationItemPlctag>();
            ValueTypes = new ObservableCollection<LookupItem<short>>();
            Datablocks = new ObservableCollection<LookupItem<int>>();
            UnitCategories = new ObservableCollection<LookupItem<int>>();

            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Subscribe(AfterDatablockSaved);
            _eventAggregator.GetEvent<AfterPlctagDeletedEvent>().Subscribe(AfterDatablockDeleted);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, OnCancelCanExecute);
        }

        public async Task LoadAsync()
        {
            var lookup = await _plctagLookupDataRepository.GetPlctagLookupAsync();
            Plctags.Clear();
            foreach (var item in lookup)
            {
                item.Plctag.PropertyChanged += (s, e) =>
               {
                   if (!HasChanges)
                   {
                       HasChanges = _plctagLookupDataRepository.HasChanges();
                   }
               };
                Plctags.Add(item);
            }

            var lookup2 = await _valueTypeLookupDataRepository.GetValueTypeLookupAsync();
            ValueTypes.Clear();
            foreach (var item in lookup2)
            {
                ValueTypes.Add(item);
            }

            var lookup3 = await _datablockLookupDataRepository.GetDatablockLookupAsync();
            Datablocks.Clear();
            foreach (var item in lookup3)
            {
                Datablocks.Add(item);
            }

            var lookup4 = await _unitCategoryLookupDataRepository.GetUnitCategoryLookupAsync();
            UnitCategories.Clear();
            foreach (var item in lookup4)
            {
                UnitCategories.Add(item);
            }
        }
        private async void OnSaveExecute()
        {
            await _plctagLookupDataRepository.SaveAsync();
            HasChanges = _plctagLookupDataRepository.HasChanges();
        }


        private bool OnSaveCanExecute()
        {
            return Plctags != null && HasChanges;
        }

        private void AfterDatablockDeleted(int plctagId)
        {
            var plctag = Plctags.FirstOrDefault(p => p.Plctag.Id == plctagId);
            if (plctag != null)
            {
                Plctags.Remove(plctag);
            }
        }

        private void OnCancelExecute()
        {
            _plctagLookupDataRepository.RejectChanges();
            HasChanges = _plctagLookupDataRepository.HasChanges();

            // Refresh ObservableCollection for UI-> alternative for OnpropertyChanged
            System.Windows.Data.CollectionViewSource.GetDefaultView(Plctags).Refresh(); ;
        }

        private bool OnCancelCanExecute()
        {
            return Plctags != null && HasChanges;
        }

        private void AfterDatablockSaved(AfterPlctagSavedEventArgs eventArgs)
        {
            //var lookupItem = Plctags.FirstOrDefault(p => p.Id == eventArgs.Id);
            //if (lookupItem == null)
            //{
            //    Plctags.Add(new NavigationItemViewModel(eventArgs.Id, eventArgs.DisplayMember, _eventAggregator));
            //}
            //else
            //{
            //    lookupItem.DisplayMember = eventArgs.DisplayMember;
            //}
        }


    }
}
