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
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<NavigationItemPlctag> Plctags { get; }
        public ObservableCollection<LookupItem<short>> ValueTypes { get; }
        public ObservableCollection<LookupItem<int>> Datablocks { get; }

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


        public NavigationViewModel(IPlctagLookupDataService plctagLookupDataService, IValueTypeLookupDataService valueTypeLookupDataService, IDatablockLookupDataService datablockLookupDataService, IEventAggregator eventAggregator)
        {
            _plctagLookupDataRepository = plctagLookupDataService;
            _valueTypeLookupDataRepository = valueTypeLookupDataService;
            _datablockLookupDataRepository = datablockLookupDataService;
            _eventAggregator = eventAggregator;

            Plctags = new ObservableCollection<NavigationItemPlctag>();
            ValueTypes = new ObservableCollection<LookupItem<short>>();
            Datablocks = new ObservableCollection<LookupItem<int>>();

            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Subscribe(AfterDatablockSaved);
            _eventAggregator.GetEvent<AfterPlctagDeletedEvent>().Subscribe(AfterDatablockDeleted);

            SaveCommand = new DelegateCommand(OnSaveExecute);
        }

        private async void OnSaveExecute()
        {
            await _plctagLookupDataRepository.SaveAsync();
        }

        public async Task LoadAsync()
        {
            var lookup = await _plctagLookupDataRepository.GetPlctagLookupAsync();
            Plctags.Clear();
            foreach (var item in lookup)
            {
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
        }

        private void AfterDatablockDeleted(int plctagId)
        {
            var plctag = Plctags.FirstOrDefault(p => p.Plctag.Id == plctagId);
            if (plctag != null)
            {
                Plctags.Remove(plctag);
            }
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
