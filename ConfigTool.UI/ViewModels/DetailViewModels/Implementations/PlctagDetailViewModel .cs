using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConfigTool.UI.ViewModels
{
    public class PlctagDetailViewModel : GenericDetailViewModel<Plctag, int, PlctagWrapper>, IPlctagDetailViewModel
    {
        public PlctagDetailViewModel(IPlctagRepository plctagRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(plctagRepository, eventAggregator, messageDialogService)
        {
        }
    }
}