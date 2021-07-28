using ConfigTool.Models;
using ConfigTool.Models.Interfaces;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConfigTool.UI.ViewModels
{
    public abstract class GenericDetailViewModel<TEntity, TId, TWrapper> : ViewModelBase
        where TWrapper : ModelWrapper<TEntity, TId>
        where TEntity : IEntity<TId>


    {
        #region Fields

        protected readonly IGenericRepository<TEntity, TId, TWrapper> _entityRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private TWrapper _entity;
        private bool _hasChanges;

        #endregion

        #region Properties

        public TWrapper Entity
        {
            get { return _entity; }
            private set
            {
                if (_entity != value)
                { _entity = value; }
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ObservableCollection<LookupItem<TId>> Entities { get; }

        public bool HasChanges
        {
            get { return _hasChanges; }
            protected set
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

        public GenericDetailViewModel(IGenericRepository<TEntity, TId, TWrapper> entityRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _entityRepository = entityRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);

        }

        #endregion

        #region Methods

        private async void OnSaveExecute()
        {
            try
            {
                await _entityRepository.SaveAsync();
                HasChanges = _entityRepository.HasChanges();
                _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Publish(new AfterPlctagSavedEventArgs
                {
                    Id = Convert.ToInt32(Entity.Id)
                });
            }
            catch (Exception e)
            {

                _messageDialogService.ShowErrorDialog(e.Message + "\n\n" + e.InnerException, "Saving Failed");
            }
        }

        private bool OnSaveCanExecute()
        {
            return Entity != null && !Entity.HasErrors && HasChanges;
        }
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete ID = {Entity.Id} ?", "Question");
            if (result == MessageDialogResult.OK)
            {
                try
                {
                    _entityRepository.Remove(Entity.Model);
                    await _entityRepository.SaveAsync();
                    _eventAggregator.GetEvent<TagDeletedEvent>().Publish(Convert.ToInt32(Entity.Id));
                }
                catch (Exception e)
                {
                    _messageDialogService.ShowErrorDialog(e.Message + "\n\n" + e.InnerException, "Removal Failed");
                }
            }
        }

        public async virtual Task LoadAsync(EventParameters? eventParameters)
        {
            var item = eventParameters != null && eventParameters.Id != 0 ? await _entityRepository.GetByIdAsync(ConvertValue<TId, int>(eventParameters.Id)) : CreateNewItem();

            InitializeDatablock(item);

        }

        protected void InitializeDatablock(TEntity entity)
        {
            Entity = (TWrapper)Activator.CreateInstance(typeof(TWrapper), new object[] { entity });

            Entity.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _entityRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Entity.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            //if (Convert.ToInt32(datablock.Id) == 0)
            //{
            //    //Little trick to trigger the validation 
            //    datablock.Name = "";
            //}
        }

        private async Task LoadDatablocksLookupAsync()
        {
            Entities.Clear();
            var lookup = await _entityRepository.GetAllLookupAsync();
            foreach (var lookupItem in lookup)
            {
                //Datablocks.Add(ConvertValue<LookupItem<TId>, LookupItem<int>>(lookupItem));
                Entities.Add(lookupItem);
            }
        }

        protected TEntity CreateNewItem()
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = ConvertValue<TId, int>(Convert.ToInt32(_entityRepository.GetMaxId()) + 1);
            _entityRepository.Add(entity);
            return entity;
        }

        private T ConvertValue<T, U>(U value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        #endregion

    }
}
