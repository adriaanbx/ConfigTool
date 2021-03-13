using ConfigTool.Models;
using ConfigTool.UI.Repositories;
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


        public PlctagDetailViewModel(IPlctagRepository plctagRepository)
        {
            _plctagRepository = plctagRepository;
        }

        public async Task LoadAsync(int plctagId)
        {
            Plctag = await _plctagRepository.GetByIdAsync(plctagId);
        }
    }
}
