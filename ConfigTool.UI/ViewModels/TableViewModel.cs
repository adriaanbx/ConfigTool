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
using System.ComponentModel;
using System.Diagnostics;

namespace ConfigTool.UI.ViewModels
{
    public class TableViewModel : ViewModelBase, ITableViewModel
    {
        private readonly IPlctagLookupDataService _plctagLookupDataRepository;
        private readonly IValueTypeLookupDataService _valueTypeLookupDataRepository;
        private readonly IDatablockLookupDataService _datablockLookupDataRepository;
        private readonly IUnitCategoryLookupDataService _unitCategoryLookupDataRepository;
        private readonly ITextLanguageLookupDataService _textLanguageLookupDataRepository;
        private readonly IEventAggregator _eventAggregator;
        private bool _hasChanges;

        public ObservableCollection<TableItemPlctag> Plctags { get; }
        public ICollectionView PlctagCollectionView { get; }
        public ObservableCollection<LookupItem<short>> ValueTypes { get; }
        public ObservableCollection<LookupItem<int>> Datablocks { get; }
        public ObservableCollection<LookupItem<int>> UnitCategories { get; }
        public ObservableCollection<LookupItem<int>> TextLanguages { get; }

        private string _plctagFilter = string.Empty;

        public string PlctagFilter
        {
            get { return _plctagFilter; }
            set
            {
                if (_plctagFilter != value)
                {
                    _plctagFilter = value;
                    OnPropertyChanged();
                    PlctagCollectionView.Refresh();
                }
            }
        }


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
                    ((DelegateCommand)CreateNewPlctagCommand).RaiseCanExecuteChanged();
                }

            }
        }

        private TableItemPlctag _selectedCell;

        public TableItemPlctag SelectedCell
        {
            get { return _selectedCell; }
            set
            {
                if (value.ColumnName != null && _selectedCell != value)
                {
                    _selectedCell = value;

                    //get selected column name
                    var columnName = SelectedCell.ColumnName;

                    //initialisation
                    var isForeignKeyColumn = false;

                    //selected column is Text column
                    if (columnName.Equals("Text"))
                    {
                        var TextId = SelectedCell.Plctag.TextId;

                        //Publish event to subscribers
                        _eventAggregator.GetEvent<OpenDetailViewEvent>()
                            .Publish(new EventParameters() { Id = Convert.ToInt32(TextId), TableName = columnName });
                    }

                    else
                    {
                        //get foreign keys
                        var foreignKeys = _plctagLookupDataRepository.GetForeignKeys();

                        //Open details view when selected column is a foreign key
                        foreach (var key in foreignKeys)
                        {
                            if (key.PrincipalEntityType.ToString().Contains(columnName))
                            {
                                //selected column is foreign key column
                                isForeignKeyColumn = true;

                                //get primarykey column name of foreignkey table
                                var primaryKeyColumnName = columnName + "Id";

                                //use reflection to get the value of the property, aka selected column, at runtime
                                var primaryKeyValue = SelectedCell.Plctag.GetType().GetProperty(primaryKeyColumnName)?.GetValue(SelectedCell.Plctag);

                                //Publish event to subscribers
                                _eventAggregator.GetEvent<OpenDetailViewEvent>()
                                    .Publish(new EventParameters() { Id = Convert.ToInt32(primaryKeyValue), TableName = columnName });

                                break;
                            }
                        }

                        //selected column is not a foreign key column
                        if (!isForeignKeyColumn)
                        {
                            //use reflection to get the value of the property, aka selected column, at runtime
                            var primaryKeyValue = SelectedCell.Plctag.Id;

                            //Publish event to subscribers
                            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                                .Publish(new EventParameters() { Id = Convert.ToInt32(primaryKeyValue), TableName = columnName });
                        }
                    }


                    OnPropertyChanged();

                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CreateNewPlctagCommand { get; }

        public TableViewModel(IPlctagLookupDataService plctagLookupDataService, IValueTypeLookupDataService valueTypeLookupDataService,
                                   IDatablockLookupDataService datablockLookupDataService, IUnitCategoryLookupDataService unitCategoryLookupDataService,
                                   ITextLanguageLookupDataService textLanguageLookupDataService, IEventAggregator eventAggregator)
        {
            _plctagLookupDataRepository = plctagLookupDataService;
            _valueTypeLookupDataRepository = valueTypeLookupDataService;
            _datablockLookupDataRepository = datablockLookupDataService;
            _unitCategoryLookupDataRepository = unitCategoryLookupDataService;
            _textLanguageLookupDataRepository = textLanguageLookupDataService;
            _eventAggregator = eventAggregator;

            Plctags = new ObservableCollection<TableItemPlctag>();
            ValueTypes = new ObservableCollection<LookupItem<short>>();
            Datablocks = new ObservableCollection<LookupItem<int>>();
            UnitCategories = new ObservableCollection<LookupItem<int>>();
            TextLanguages = new ObservableCollection<LookupItem<int>>();

            PlctagCollectionView = System.Windows.Data.CollectionViewSource.GetDefaultView(Plctags);
            PlctagCollectionView.Filter += FilterPlctags;

            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Subscribe(RefreshObservableCollection);
            _eventAggregator.GetEvent<AfterPlctagDeletedEvent>().Subscribe(AfterDatablockDeleted);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, OnCancelCanExecute);
            CreateNewPlctagCommand = new DelegateCommand(OnCreateNewPlctagExecute, OnCreateNewPlctagCanExecute);
        }

        private bool OnCreateNewPlctagCanExecute()
        {
            return Plctags != null && !HasChanges;
        }

        private void OnCreateNewPlctagExecute()
        {
            //Publish event to subscribers
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Publish(null);
        }

        private bool FilterPlctags(object obj)
        {
            var boolResult = false;

            if (obj is TableItemPlctag tableItemPlctag)
            {
                //Show all tags when filter is empty
                if (string.IsNullOrEmpty(PlctagFilter))
                {
                    return true;
                }

                //Check all columns with 'number' datatype
                if (Int32.TryParse(PlctagFilter, out int result))
                {
                    return tableItemPlctag.Plctag.Id.Equals(result) ||
                           //tableItemPlctag.Plctag.ArraySize.Equals(result) ||
                           tableItemPlctag.Plctag.CycleTime.Equals(result);
                }

                //Check nullable columns
                boolResult = string.IsNullOrEmpty(tableItemPlctag.Plctag.Name) ? false : tableItemPlctag.Plctag.Name.Contains(PlctagFilter, StringComparison.InvariantCultureIgnoreCase);
                if (boolResult == true)
                {
                    return true;
                }

                boolResult = string.IsNullOrEmpty(tableItemPlctag.Text) ? false : tableItemPlctag.Text.Contains(PlctagFilter, StringComparison.InvariantCultureIgnoreCase);
                if (boolResult == true)
                {
                    return true;
                }

                //Todo Ternary if-statement doesn't work together with or-statement?
                //Check all columns with 'varchar' datatype
                return tableItemPlctag.Plctag.DefaultValue.Contains(PlctagFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItemPlctag.Plctag.Number.Contains(PlctagFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItemPlctag.DataBlock.Contains(PlctagFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItemPlctag.ValueType.Contains(PlctagFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItemPlctag.UnitCategory.Contains(PlctagFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       //string.IsNullOrEmpty(tableItemPlctag.Plctag.Name) ? false : tableItemPlctag.Plctag.Name.Contains(PlctagFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       //string.IsNullOrEmpty(tableItemPlctag.Text) ? false : tableItemPlctag.Text.Contains(PlctagFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItemPlctag.Plctag.Log.ToString().Contains(PlctagFilter, StringComparison.InvariantCultureIgnoreCase);

            }
            return false;
        }

        public async Task LoadAsync()
        {
            var timer = new Stopwatch();
            timer.Start();

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

            var lookup5 = await _textLanguageLookupDataRepository.GetTextLanguageLookupAsync();
            TextLanguages.Clear();
            foreach (var item in lookup5)
            {
                TextLanguages.Add(item);
            }

            timer.Stop();
            Debug.WriteLine(timer.Elapsed.ToString(@"m\:ss\.fff"));
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
            RefreshObservableCollection();
        }

        private async void RefreshObservableCollection(AfterPlctagSavedEventArgs? eventArgs)
        {
            _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Loading...");
            await LoadAsync();
            PlctagCollectionView.Refresh();
            _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Ready");

        }

        private void RefreshObservableCollection()
        {
            PlctagCollectionView.Refresh();
        }

        private bool OnCancelCanExecute()
        {
            return Plctags != null && HasChanges;
        }

        private void AfterDatablockSaved(AfterPlctagSavedEventArgs? eventArgs)
        {
            //var lookupItem = Plctags.FirstOrDefault(p => p.Id == eventArgs.Id);
            //if (lookupItem == null)
            //{
            //    Plctags.Add(new TableItemViewModel(eventArgs.Id, eventArgs.DisplayMember, _eventAggregator));
            //}
            //else
            //{
            //    lookupItem.DisplayMember = eventArgs.DisplayMember;
            //}
        }


    }
}
