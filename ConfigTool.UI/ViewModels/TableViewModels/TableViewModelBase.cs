using ConfigTool.Models.Interfaces;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
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
        protected readonly IGenericRepository<TEntity, TId, TWrapper> _tableRepository;
        protected readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
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
                    ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
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

                    //Trigger Delete button
                    ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();

                    //get selected column name
                    var columnName = SelectedCell.ColumnName;

                    //initialisation
                    var isForeignKeyColumn = false;

                    //selected column is Text column
                    if (columnName.Contains("Text"))
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
                            if (key.PrincipalEntityType.ToString().ToLower().Contains(columnName.ToLower()))
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
            throw new NotImplementedException();
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CreateNewTableItemCommand { get; }
        public ICommand DeleteCommand { get; }

        public TableViewModelBase(IGenericRepository<TEntity, TId, TWrapper> tableRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _tableRepository = tableRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            TableItems = new RangeObservableCollection<TableItem<TEntity, TId, TWrapper>>();

            TableCollectionView = System.Windows.Data.CollectionViewSource.GetDefaultView(TableItems);
            TableCollectionView.Filter += FilterTags;

            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Subscribe(RefreshObservableCollection);
            _eventAggregator.GetEvent<TagDeletedEvent>().Subscribe(AfterDatablockDeleted);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, OnCancelCanExecute);
            CreateNewTableItemCommand = new DelegateCommand(OnCreateNewTableItemExecute, OnCreateNewTableItemCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);
        }

        private bool OnDeleteCanExecute()
        {
            return TableItems != null && SelectedCell != null && !HasChanges;
        }

        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete ID = {SelectedCell.Table.Id} ?", "Confirmation");
            if (result == MessageDialogResult.OK)
            {
                //update statusbar
                _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Deleting in progress...");

                try
                {
                    //Remove item
                    _tableRepository.Remove(_selectedCell.Table.Model);
                    await _tableRepository.SaveAsync();

                    //Clean Detail and Table view
                    _eventAggregator.GetEvent<TagDeletedEvent>().Publish(Convert.ToInt32(SelectedCell.Table.Id));
                    RefreshObservableCollection();


                    //update statusbar
                    _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Ready");
                }
                catch (Exception e)
                {

                    _messageDialogService.ShowErrorDialog(e.Message + "\n\n" + e.InnerException, "Removal Failed");

                    //update statusbar
                    _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Removal Failed");
                }
            }
        }

        private bool OnCreateNewTableItemCanExecute()
        {
            return TableItems != null && !HasChanges;
        }

        private void OnCreateNewTableItemExecute()
        {
            //Publish event to subscribers
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Publish(new EventParameters { TableName = typeof(TEntity).Name });
        }

        protected abstract bool FilterTags(object obj);
        private async void OnSaveExecute()
        {
            //update statusbar
            _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Saving in progress...");

            try
            {
                await _tableRepository.SaveAsync();
                HasChanges = _tableRepository.HasChanges();

                //update statusbar
                _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Ready");
            }
            catch (Exception e)
            {

                _messageDialogService.ShowErrorDialog(e.Message + "\n\n" + e.InnerException, "Saving Failed");

                //update statusbar
                _eventAggregator.GetEvent<StatusChangedEvent>().Publish("Saving Failed");
            }

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
