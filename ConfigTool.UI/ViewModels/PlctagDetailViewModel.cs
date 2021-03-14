using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
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
        private Plctag _plctag;
        #endregion

        #region Properties

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
            Plctag = await _plctagRepository.GetByIdAsync(plctagId);
        }
        #endregion

    }
}
