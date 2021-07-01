using ConfigTool.UI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels
{
    public interface IDetailViewModel
    {
        public bool HasChanges { get;}
        public Task LoadAsync(EventParameters? eventParameters);
    }
}
