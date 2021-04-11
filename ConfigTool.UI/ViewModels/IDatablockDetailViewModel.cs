using ConfigTool.UI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModel
{
    public interface IDatablockDetailViewModel
    {
        Task LoadAsync(EventParameters? eventParameters);
        bool HasChanges { get; }
    }
}
