using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Events;

namespace ConfigTool.UI.ViewModels
{
    public class UnitCategoryDetailViewModel : GenericDetailViewModel<UnitCategory, int, UnitCategoryWrapper>, IUnitCategoryDetailViewModel
    {
        public UnitCategoryDetailViewModel(IUnitCategoryRepository unitCategoryRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(unitCategoryRepository, eventAggregator, messageDialogService)
        {
        }
    }
}
