using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModel
{
    public class NavigationViewModel : INavigationViewModel
    {
        private readonly IPlctagLookupDataRepository _plctagLookupDataRepository;

        public ObservableCollection<LookupItem> Plctags { get; }

        public NavigationViewModel(IPlctagLookupDataRepository plctagLookupDataRepository)
        {
            _plctagLookupDataRepository = plctagLookupDataRepository;
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
