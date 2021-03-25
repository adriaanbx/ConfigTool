using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.ViewModels;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IPlctagLookupDataRepository _plctagLookupDataRepository;
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<NavigationItemViewModel> Plctags { get; }

        public NavigationViewModel(IPlctagLookupDataRepository plctagLookupDataRepository, IEventAggregator eventAggregator)
        {
            _plctagLookupDataRepository = plctagLookupDataRepository;
            _eventAggregator = eventAggregator;
            Plctags = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterPlctagSavedEvent>()
                .Subscribe(AfterPlctagSaved);
        }

        private void AfterPlctagSaved(AfterPlctagSavedEventArgs eventArgs)
        {
            var lookupItem = Plctags.First(p => p.Id == eventArgs.Id);
            lookupItem.DisplayMember = eventArgs.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _plctagLookupDataRepository.GetPlctagLookupAsync();
            Plctags.Clear();
            foreach (var item in lookup)
            {
                Plctags.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }


    }
}
