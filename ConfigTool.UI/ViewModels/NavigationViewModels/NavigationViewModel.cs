using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IModelRepository _modelRepository;
        private readonly IEventAggregator _eventAggregator;
        private bool _hasChanges;
        private ObservableCollection<string> _tables;


        private string _selectedTable;

        public string SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                if (_selectedTable != value)
                {
                    _selectedTable = value;

                    //Publish event to subscribers
                    _eventAggregator.GetEvent<OpenTableViewEvent>()
                        .Publish(new EventParameters() { TableName = SelectedTable });

                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Tables { get; set; }

        public bool HasChanges
        {
            get { return _hasChanges; }
            protected set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                }
            }
        }

        public NavigationViewModel(IModelRepository modelRepository, IEventAggregator eventAggregator)
        {
            _modelRepository = modelRepository;
            _eventAggregator = eventAggregator;

            Tables = new ObservableCollection<string>();
        }

        public async Task LoadAsync()
        {
            var tables = _modelRepository.GetAllTables();
            foreach (var table in tables)
            {
                Tables.Add(table);
            }
            
            //TODO Na merge de AddRange van de RangeObservableCollection gebruiken
            //Tables.AddRange(_modelRepository.GetAllTables());
        }
    }
}
