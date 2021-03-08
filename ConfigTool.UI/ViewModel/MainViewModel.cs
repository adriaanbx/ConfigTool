using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPlcTagRepository _plcTagRepository;
        private Plctag _selectedPlctag;

        public ObservableCollection<Plctag> PlcTags { get; set; }

        public Plctag SelectedPlctag
        {
            get { return _selectedPlctag; }
            set
            {
                if (_selectedPlctag != value)
                {
                    _selectedPlctag = value;
                    OnPropertyChanged();
                }
            }
        }


        public MainViewModel(IPlcTagRepository plcTagRepository)
        {
            PlcTags = new ObservableCollection<Plctag>();
            _plcTagRepository = plcTagRepository;
        }

        public async Task LoadAsync()
        {
            var plcTags = await _plcTagRepository.GetAllAsync();
            PlcTags.Clear();
            foreach (var plctag in plcTags)
            {
                PlcTags.Add(plctag);
            }
        }
    }
}
