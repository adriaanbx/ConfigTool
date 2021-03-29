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
            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Subscribe(AfterPlctagSaved);
            _eventAggregator.GetEvent<AfterPlctagDeletedEvent>().Subscribe(AfterPlctagDeleted);
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

        private void AfterPlctagDeleted(int plctagId)
        {
            var plctag = Plctags.FirstOrDefault(p => p.Id == plctagId);
            if(plctag != null)
            {
                Plctags.Remove(plctag);
            }            
        }

        private void AfterPlctagSaved(AfterPlctagSavedEventArgs eventArgs)
        {
            var lookupItem = Plctags.FirstOrDefault(p => p.Id == eventArgs.Id);
            if (lookupItem == null)
            {
                Plctags.Add(new NavigationItemViewModel(eventArgs.Id, eventArgs.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = eventArgs.DisplayMember;
            }
        }


    }
}
