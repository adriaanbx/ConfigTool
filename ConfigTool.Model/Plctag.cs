using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Plctag
    {
        public int? ArraySize { get; set; }
        public string DefaultValue { get; set; }
        public string Statistics { get; set; }
        public string Screensaver { get; set; }
        public int? CycleTime { get; set; }

        public string Name { get; set; }
        public string Number { get; set; }
        public int Id { get; set; }
        public int? TextId { get; set; }
        public int? DataBlockId { get; set; }
        public short ValueTypeId { get; set; }
        public int? UnitCategoryId { get; set; }
    }
}
