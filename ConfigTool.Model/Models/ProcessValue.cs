using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class ProcessValue
    {
        public short? ProcessValueCalculationTypeId { get; set; }
        public int Id { get; set; }
        public int? TagId { get; set; }
        public int? TextId { get; set; }
        public int? GroupTextId { get; set; }
        public string Visible { get; set; }
        public int? SortPosition { get; set; }
    }
}
