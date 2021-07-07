using ConfigTool.UI.Events;
using ConfigTool.UI.Views.Services;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConfigTool.UI.ViewModels
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        private bool _hasChanges;
        protected readonly IEventAggregator EventAggregator;
        private readonly IMessageDialogService _messageDialogService;

        public DetailViewModelBase(IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            EventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            _messageDialogService = messageDialogService;
        }


        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

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
        public abstract Task LoadAsync(EventParameters? eventParameters);

        protected abstract void OnDeleteExecute();

        protected abstract bool OnSaveCanExecute();

        protected abstract void OnSaveExecute();

        //protected virtual void RaiseDetailDeletedEvent(int modelId)
        //{
        //    EventAggregator.GetEvent<AfterDetailDeletedEvent>().Publish(new
        //     AfterDetailDeletedEventArgs
        //    {
        //        Id = modelId,
        //        ViewModelName = this.GetType().Name
        //    });
        //}

        //protected virtual void RaiseDetailSavedEvent(int modelId, string displayMember)
        //{
        //    EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(new AfterDetailSavedEventArgs
        //    {
        //        Id = modelId,
        //        DisplayMember = displayMember,
        //        ViewModelName = this.GetType().Name
        //    });
        //}

    }
}
