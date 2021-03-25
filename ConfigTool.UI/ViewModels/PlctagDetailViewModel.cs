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
        private bool _hasChanges;
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

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

            }
        }
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
            HasChanges = _plctagRepository.HasChanges();
            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Publish(new AfterPlctagSavedEventArgs
            {
                Id = Plctag.Id,
                DisplayMember = Plctag.Name
            });
        }

        private bool OnSaveCanExecute()
        {
            return Plctag != null && !Plctag.HasErrors && HasChanges;
        }

        public async Task LoadAsync(int plctagId)
        {
            var plctag = await _plctagRepository.GetByIdAsync(plctagId);
            Plctag = new PlctagWrapper(plctag);

            Plctag.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _plctagRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Plctag.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
        #endregion

    }
}
