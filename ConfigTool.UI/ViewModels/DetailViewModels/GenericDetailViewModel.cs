﻿using ConfigTool.Models;
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
        where TWrapper : ModelWrapper<TEntity>
        where TEntity :  IEntity<TId>
        

    {
        #region Fields

        protected readonly IGenericRepository<TEntity, TId> _entityRepository;
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

        public GenericDetailViewModel(IGenericRepository<TEntity, TId> entityRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
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
            await _entityRepository.SaveAsync();
            HasChanges = _entityRepository.HasChanges();
            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Publish(new AfterPlctagSavedEventArgs
            {
                Id = Entity.Id,
                //DisplayMember = Datablock.Name
            });
        }

        private bool OnSaveCanExecute()
        {
            return Entity != null && !Entity.HasErrors && HasChanges;
        }
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the pcltag {Entity.Id} ?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _entityRepository.Remove(Entity.Model);
                await _entityRepository.SaveAsync();
                _eventAggregator.GetEvent<TagDeletedEvent>().Publish(Entity.Id);
            }
        }

        public async virtual Task LoadAsync(EventParameters? eventParameters)
        {
            var datablock = eventParameters != null ? await _entityRepository.GetByIdAsync(ConvertValue<TId, int>(eventParameters.Id)) : CreateNewDatablock();

            InitializeDatablock(datablock);

        }

        protected void InitializeDatablock(TEntity entity)
        {
            Entity = (TWrapper) Activator.CreateInstance(typeof(TWrapper), new object[] { entity });

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

        protected TEntity CreateNewDatablock()
        {
            var entity = Activator.CreateInstance<TEntity>();
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
