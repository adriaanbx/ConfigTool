using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Events;

namespace ConfigTool.UI.ViewModels
{
    public class LayerSideDetailViewModel : GenericDetailViewModel<LayerSide, short, LayerSideWrapper>, ILayerSideDetailViewModel
    {
        public LayerSideDetailViewModel(ILayerSideRepository layerSideRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(layerSideRepository, eventAggregator, messageDialogService)
        {
        }
    }
}
