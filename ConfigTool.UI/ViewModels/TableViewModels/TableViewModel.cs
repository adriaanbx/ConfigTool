using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ConfigTool.UI.Lookups;
using System.Windows.Input;
using Prism.Commands;
using System.ComponentModel;
using System.Diagnostics;

namespace ConfigTool.UI.ViewModels
{
    public abstract class TableViewModel : ViewModelBase, ITableViewModel
    {
        public abstract bool HasChanges { get; }

        public abstract Task LoadAsync(EventParameters? eventParameters);
    }
}