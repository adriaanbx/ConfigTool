using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModel
{
    public interface IPlctagDetailViewModel
    {
        Task LoadAsync(int? plctagId);
        bool HasChanges { get; }
    }
}
