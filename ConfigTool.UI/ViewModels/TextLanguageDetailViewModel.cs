using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Lookups;
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
    public class TextLanguageDetailViewModel : ViewModelBase, ITextLanguageDetailViewModel
    {
        #region Fields

        private readonly ITextLanguageRepository _textLanguageRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly ITextLanguageLookupDataService _textLanguageLookupDataRepository;
        private TextLanguageWrapper _textLanguage;
        private bool _hasChanges;

        #endregion

        #region Properties

        public TextLanguageWrapper TextLanguage
        {
            get { return _textLanguage; }
            private set
            {
                if (_textLanguage != value)
                { _textLanguage = value; }
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ObservableCollection<LookupItem<int>> TextLanguages { get; }

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

        public TextLanguageDetailViewModel(ITextLanguageRepository textLanguageRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService, ITextLanguageLookupDataService textLanguageLookupDataRepository)
        {
            _textLanguageRepository = textLanguageRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _textLanguageLookupDataRepository = textLanguageLookupDataRepository;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);

            _eventAggregator.GetEvent<ComboboxSelectionChangedEvent>().Subscribe(AfterComboboxChanged);
        }

        #endregion

        #region Methods

        private async void OnSaveExecute()
        {
            await _textLanguageRepository.SaveAsync();
            HasChanges = _textLanguageRepository.HasChanges();
            _eventAggregator.GetEvent<AfterPlctagSavedEvent>().Publish(new AfterPlctagSavedEventArgs
            {
                Id = TextLanguage.TextId,
                DisplayMember = TextLanguage.Text
            });
        }

        private bool OnSaveCanExecute()
        {
            return TextLanguage != null && !TextLanguage.HasErrors && HasChanges;
        }
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the text {TextLanguage.TextId} {TextLanguage.Text}?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _textLanguageRepository.Remove(TextLanguage.Model);
                await _textLanguageRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterPlctagDeletedEvent>().Publish(TextLanguage.TextId);
            }
        }

        public async Task LoadAsync(EventParameters? eventParameters)
        {
            var textLanguage = eventParameters != null ? await _textLanguageRepository.GetByTextIdAsync(eventParameters.Id) : CreateNewTextLanguage();

            InitializeTextLanguage(textLanguage);

            //await LoadTextLanguagesLookupAsync();
        }

        private void InitializeTextLanguage(TextLanguage textLanguage)
        {
            TextLanguage = new TextLanguageWrapper(textLanguage);

            TextLanguage.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _textLanguageRepository.HasChanges();
                }

                if (e.PropertyName == nameof(TextLanguage.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (textLanguage.Id == 0)
            {
                //Little trick to trigger the validation 
                textLanguage.Text = "";
            }
        }

        private async Task LoadTextLanguagesLookupAsync()
        {
            TextLanguages.Clear();
            var lookup = await _textLanguageLookupDataRepository.GetTextLanguageLookupAsync();
            foreach (var lookupItem in lookup)
            {
                TextLanguages.Add(lookupItem);
            }
        }

        private TextLanguage CreateNewTextLanguage()
        {
            var textLanguage = new TextLanguage();
            _textLanguageRepository.Add(textLanguage);
            return textLanguage;
        }

        private async void AfterComboboxChanged(EventParameters? eventParameters)
        {
            var textLanguage = eventParameters != null ? await _textLanguageRepository.GetByTextIdAsync(eventParameters.Id) : CreateNewTextLanguage();
            //TODO moet op een of andere manier generiek gemaakt worden
            TextLanguage.TextId = textLanguage.TextId;
            TextLanguage.Text = textLanguage.Text;
        }
        #endregion

    }
}
