using ConfigTool.UI.Repositories;
using ConfigTool.UI.Views.Services;
using ConfigTool.UI.Wrappers;
using Prism.Events;
using ValueType = ConfigTool.Models.ValueType;

namespace ConfigTool.UI.ViewModels
{
    public class ValueTypeDetailViewModel : GenericDetailViewModel<ValueType, short, ValueTypeWrapper>, IValueTypeDetailViewModel
    {
        public ValueTypeDetailViewModel(IValueTypeRepository valueTypeRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(valueTypeRepository, eventAggregator, messageDialogService)
        {
        }
    }
}
