using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Events;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels
{
    public class TextLanguageDetailViewModel : GenericDetailViewModel<TextLanguage, int, TextLanguageWrapper>, ITextLanguageDetailViewModel
    {
        public TextLanguageDetailViewModel(ITextLanguageRepository textLanguageRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(textLanguageRepository, eventAggregator, messageDialogService)
        {
          
        }

        public override async Task LoadAsync(EventParameters? eventParameters)
        {
            var textLanguage = eventParameters != null && eventParameters.Id != 0 ? await ((TextLanguageRepository)_entityRepository).GetByTextIdAsync(eventParameters.Id) : CreateNewItem();

            InitializeDatablock(textLanguage);
        }
    }
}
