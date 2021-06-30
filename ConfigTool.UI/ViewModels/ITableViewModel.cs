using ConfigTool.UI.Events;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels
{
    public interface ITableViewModel
    {
        public abstract bool HasChanges { get; }

        public abstract Task LoadAsync(EventParameters? eventParameters);
    }
}