using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Events;

namespace ConfigTool.UI.ViewModels
{
    public class DatablockDetailViewModel : GenericDetailViewModel<DataBlock, int, DatablockWrapper>, IDatablockDetailViewModel
    {
        public DatablockDetailViewModel(IDatablockRepository datablockRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(datablockRepository, eventAggregator, messageDialogService)
        {
        }
    }
}
