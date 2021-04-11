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
        public int Id { get; set; }
        public int? ArraySize { get; set; }
        public string DefaultValue { get; set; }
        public string Statistics { get; set; }
        public string Screensaver { get; set; }
        public int? CycleTime { get; set; }

        public string Name { get; set; }
        public string Number { get; set; }

        public int DataBlockId { get; set; }
        public short ValueTypeId { get; set; }

        //Todo kunnen we bovenstaande properties niet vervangen door PLCtag zelf? Maar dan toont datagrid enkel de Plctag klasse ipv de properties
        //public Plctag Plctag { get; set; }

        public string? DataBlock { get; set; }
        public string ValueType { get; set; }
        public string? UnitCategory { get; set; }
    }
}
