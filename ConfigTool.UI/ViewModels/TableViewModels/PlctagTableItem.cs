using ConfigTool.Models;
using ConfigTool.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels.TableViewModels
{
    public class PlctagTableItem : TableItem<Plctag, int, PlctagWrapper>
    {
        public string? DataBlock { get; set; }
        public string ValueType { get; set; }
        public string? UnitCategory { get; set; }
        public string? Text { get; set; }
    }
}
