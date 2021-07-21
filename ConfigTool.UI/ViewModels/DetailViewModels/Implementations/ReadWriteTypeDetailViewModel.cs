using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Events;

namespace ConfigTool.UI.ViewModels
{
    public class ReadWriteTypeDetailViewModel : GenericDetailViewModel<ReadWriteType, short, ReadWriteTypeWrapper>, IReadWriteTypeDetailViewModel
    {
        public ReadWriteTypeDetailViewModel(IReadWriteTypeRepository readWriteTypeRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(readWriteTypeRepository, eventAggregator, messageDialogService)
        {
        }
    }
}
