using ConfigTool.Models;
using ConfigTool.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels
{
    public class NavigationItemPlctag:ViewModelBase
    {
        public PlctagWrapper Plctag { get; set; }

        public string? DataBlock { get; set; }
        public string ValueType { get; set; }
        public string? UnitCategory { get; set; }
        public string? Text { get; set; }

        public string ColumnName { get; set; }
    }
}
