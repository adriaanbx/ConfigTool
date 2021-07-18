using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Events;

namespace ConfigTool.UI.ViewModels
{
    public class PressParameterTypeDetailViewModel : GenericDetailViewModel<PressParameterType, int, PressParameterTypeWrapper>, IPressParameterTypeDetailViewModel
    {
        public PressParameterTypeDetailViewModel(IPressParameterTypeRepository pressParameterTypeRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(pressParameterTypeRepository, eventAggregator, messageDialogService)
        {
        }
    }
}
