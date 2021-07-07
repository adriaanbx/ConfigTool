using ConfigTool.UI.Events;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels
{
    public abstract class TableViewModel : ViewModelBase, ITableViewModel
    {
        public abstract bool HasChanges { get; }

        public abstract Task LoadAsync(EventParameters? eventParameters);
    }
}