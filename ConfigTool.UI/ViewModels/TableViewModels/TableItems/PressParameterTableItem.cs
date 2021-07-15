using ConfigTool.Models;
using ConfigTool.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels.TableViewModels
{
    public class PressParameterTableItem : TableItem<PressParameter, int, PressParameterWrapper>
    {
        public string? Plctag { get; set; }
        public string? LayerSide { get; set; }
        public string? PressParameterType { get; set; }
    }
}
