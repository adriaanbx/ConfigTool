using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModel
{
    public class PlctagDetailViewModel : ViewModelBase, IPlctagDetailViewModel
    {
        private readonly IPlctagRepository _plctagRepository;
        private readonly IEventAggregator _eventAggregator;
        private Plctag _plctag;

        public Plctag Plctag
        {
            get { return _plctag; }
            private set
            {
                if (_plctag != value)
                { _plctag = value; }
                OnPropertyChanged();
            }
        }


        public PlctagDetailViewModel(IPlctagRepository plctagRepository, IEventAggregator eventAggregator)
        {
            _plctagRepository = plctagRepository;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenPlctagDetailViewEvent>()
                .Subscribe(OnOpenPlctagDetailView);
        }

        private async void OnOpenPlctagDetailView(int plctagId)
        {
             await LoadAsync(plctagId);
        }

        public async Task LoadAsync(int plctagId)
        {
            Plctag = await _plctagRepository.GetByIdAsync(plctagId);
        }
    }
}
