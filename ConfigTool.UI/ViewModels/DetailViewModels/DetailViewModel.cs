using ConfigTool.UI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels
{
    public abstract class DetailViewModel : ViewModelBase, IDetailViewModel
    {
        public abstract bool HasChanges { get;}
      
        public abstract Task LoadAsync(EventParameters? eventParameters);
    }
}
