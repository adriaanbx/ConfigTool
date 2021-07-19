using ConfigTool.Models;
using ConfigTool.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels.TableViewModels
{
    public class EngineeringTableItem : TableItem<Engineering, int, EngineeringWrapper>
    {
        public string? Plctag { get; set; }
        public string ValueType { get; set; }
        public string? ReadWriteType { get; set; }
        public string? Text { get; set; }
        public string? GroupText { get; set; }
    }
}
