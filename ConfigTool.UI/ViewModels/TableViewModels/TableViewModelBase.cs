using ConfigTool.Models.Interfaces;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConfigTool.UI.ViewModels.TableViewModels
{
    public abstract class TableViewModelBase<TEntity, TId, TWrapper> : ViewModelBase, ITableViewModel
         where TWrapper : ModelWrapper<TEntity, TId>
         where TEntity : IEntity<TId>
    {
        protected readonly IGenericRepository<TEntity,TId, TWrapper> _tableRepository;
        protected readonly IEventAggregator _eventAggregator;
        protected bool _hasChanges;

        public RangeObservableCollection<TableItem<TEntity, TId, TWrapper>> TableItems { get; }
        public ICollectionView TableCollectionView { get; }

        private string _tableFilter = string.Empty;

        public string TableFilter
        {
            get { return _tableFilter; }
            set
            {
                if (_tableFilter != value)
                {
                    _tableFilter = value;
                    OnPropertyChanged();
                    TableCollectionView.Refresh();
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
                    ((DelegateCommand)CreateNewTableItemCommand).RaiseCanExecuteChanged();
                }

            }
        }

        protected TableItem<TEntity, TId, TWrapper> _selectedCell;

        public TableItem<TEntity, TId, TWrapper> SelectedCell
        {
            get { return _selectedCell; }
            set
            {
                if (value?.ColumnName != null && _selectedCell != value)
                {
                    _selectedCell = value;

                    //get selected column name
                    var columnName = SelectedCell.ColumnName;

                    //initialisation
                    var isForeignKeyColumn = false;

                    //selected column is Text column
                    if (columnName.Equals("Text"))
                    {
                        GetTextId(columnName);
                    }

                    else
                    {
                        //get foreign keys
                        var foreignKeys = _tableRepository.GetForeignKeys();

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
                                var primaryKeyValue = SelectedCell.Table.GetType().GetProperty(primaryKeyColumnName)?.GetValue(SelectedCell.Table);

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
                            var primaryKeyValue = SelectedCell.Table.Id;

                            //Publish event to subscribers
                            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                                .Publish(new EventParameters() { Id = Convert.ToInt32(primaryKeyValue), TableName = columnName });
                        }
                    }


                    OnPropertyChanged();

                }
            }
        }

        protected virtual void GetTextId(string columnName)
        {
           new NotImplementedException();
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CreateNewTableItemCommand { get; }

        public TableViewModelBase(IGenericRepository<TEntity, TId, TWrapper> tableRepository, IEventAggregator eventAggregator)
        {
            _tableRepository = tableRepository;
            _eventAggregator = eventAggregator;

            TableItems = new RangeObservableCollection<TableItem<TEntity, TId, TWrapper>>();

            TableCollectionView = System.Windows.Data.CollectionViewSource.GetDefaultView(TableItems);
            TableCollectionView.Filter += FilterTags;

            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Subscribe(RefreshObservableCollection);
            _eventAggregator.GetEvent<TagDeletedEvent>().Subscribe(AfterDatablockDeleted);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, OnCancelCanExecute);
            CreateNewTableItemCommand = new DelegateCommand(OnCreateNewTableItemExecute, OnCreateNewTableItemCanExecute);
        }

        private bool OnCreateNewTableItemCanExecute()
        {
            return TableItems != null && !HasChanges;
        }

        private void OnCreateNewTableItemExecute()
        {
            //Publish event to subscribers
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Publish(new EventParameters { TableName = nameof(TEntity) });
        }

        protected abstract bool FilterTags(object obj);
        private async void OnSaveExecute()
        {
            //update statusbar
            _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Saving in progress...");

            await _tableRepository.SaveAsync();
            HasChanges = _tableRepository.HasChanges();

            //update statusbar
            _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Ready");
        }


        private bool OnSaveCanExecute()
        {
            return TableItems != null && HasChanges;
        }

        private void AfterDatablockDeleted(int plctagId)
        {
            var plctag = TableItems.FirstOrDefault(p => p.Table.Id.Equals(plctagId));
            if (plctag != null)
            {
                TableItems.Remove(plctag);
            }
        }

        private async void OnCancelExecute()
        {
            //update statusbar
            _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Cancellation in progress...");

            _tableRepository.RejectChanges();
            HasChanges = _tableRepository.HasChanges();

            // Refresh ObservableCollection for UI-> alternative for OnpropertyChanged
            RefreshObservableCollection();
            //TODO Hoe kan je dit op apparte thread krijgen zodat je status bar kan updaten?
            //await Task.Run(() => RefreshObservableCollection());

            //update statusbar
            _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Ready");
        }

        private async void RefreshObservableCollection(AfterPlctagSavedEventArgs? eventArgs)
        {
            _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Loading...");
            await LoadAsync(null);
            TableCollectionView.Refresh();
            _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Ready");

        }

        private void RefreshObservableCollection()
        {
            TableCollectionView.Refresh();
        }

        private bool OnCancelCanExecute()
        {
            return TableItems != null && HasChanges;
        }

        public virtual async Task LoadAsync(EventParameters? eventParameters)
        {
            //Load TableItems
            var lookup = await _tableRepository.GetTableLookupAsync();
            TableItems.Clear();
            Parallel.ForEach(lookup, item =>
            {
                item.Table.PropertyChanged += (s, e) =>
                {
                    if (!HasChanges)
                    {
                        HasChanges = _tableRepository.HasChanges();
                    }
                };
            });

            TableItems.AddRange(lookup);
        }


    }

}
