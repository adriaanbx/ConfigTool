using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IPlctagLookupDataRepository _plctagLookupDataRepository;
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<LookupItem> Plctags { get; }
        private LookupItem _selectedPlctag;

        public LookupItem SelectedPlctag
        {
            get { return _selectedPlctag; }
            set
            {
                if (_selectedPlctag != value)
                {
                    _selectedPlctag = value;
                    OnPropertyChanged();
                    if (value != null)
                        //Publish event to subscribers
                        _eventAggregator.GetEvent<OpenPlctagDetailViewEvent>()
                            .Publish(_selectedPlctag.Id);
                }
            }
        }


        public NavigationViewModel(IPlctagLookupDataRepository plctagLookupDataRepository, IEventAggregator eventAggregator)
        {
            _plctagLookupDataRepository = plctagLookupDataRepository;
            _eventAggregator = eventAggregator;
            Plctags = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _plctagLookupDataRepository.GetPlctagLookupAsync();
            Plctags.Clear();
            foreach (var item in lookup)
            {
                Plctags.Add(item);
            }
        }


    }
}
