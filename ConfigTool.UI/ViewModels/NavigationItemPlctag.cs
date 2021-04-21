using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels
{
    public class NavigationItemPlctag
    {
        public Plctag Plctag { get; set; }

        public string? DataBlock { get; set; }
        public string ValueType { get; set; }
        public string? UnitCategory { get; set; }
    }
}
