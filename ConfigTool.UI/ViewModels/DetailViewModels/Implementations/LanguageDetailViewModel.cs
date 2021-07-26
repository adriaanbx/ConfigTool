using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Events;

namespace ConfigTool.UI.ViewModels
{
    public class LanguageDetailViewModel : GenericDetailViewModel<Language, int, LanguageWrapper>, ILanguageDetailViewModel
    {
        public LanguageDetailViewModel(ILanguageRepository languageRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(languageRepository, eventAggregator, messageDialogService)
        {
        }
    }
}
