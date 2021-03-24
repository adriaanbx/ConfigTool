using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConfigTool.UI.ViewModel
{
    public class PlctagDetailViewModel : ViewModelBase, IPlctagDetailViewModel
    {
        #region Fields

        private readonly IPlctagRepository _plctagRepository;
        private readonly IEventAggregator _eventAggregator;
        private PlctagWrapper _plctag;
        #endregion

        #region Properties

        public PlctagWrapper Plctag
        {
            get { return _plctag; }
            private set
            {
                if (_plctag != value)
                { _plctag = value; }
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        #endregion

        #region Constructors

        public PlctagDetailViewModel(IPlctagRepository plctagRepository, IEventAggregator eventAggregator)
        {
            _plctagRepository = plctagRepository;
            _eventAggregator = eventAggregator;
            
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }
        #endregion

        #region Methods

        private async void OnSaveExecute()
        {
            await _plctagRepository.SaveAsync();
            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Publish(new AfterPlctagSavedEventArgs
            {
                Id = Plctag.Id,
                DisplayMember = Plctag.Name
            });
        }

        private bool OnSaveCanExecute()
        {
            //Todo: Check if Plctag is valid
            return true;
        }

        public async Task LoadAsync(int plctagId)
        {
            var plctag = await _plctagRepository.GetByIdAsync(plctagId);
            Plctag = new PlctagWrapper(plctag);
        }
        #endregion

    }
}
