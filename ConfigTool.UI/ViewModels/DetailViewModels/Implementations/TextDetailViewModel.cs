using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Events;

namespace ConfigTool.UI.ViewModels
{
    public class TextDetailViewModel : GenericDetailViewModel<Text, int, TextWrapper>, ITextDetailViewModel
    {
        public TextDetailViewModel(ITextRepository textRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(textRepository, eventAggregator, messageDialogService)
        {
        }
    }
}
