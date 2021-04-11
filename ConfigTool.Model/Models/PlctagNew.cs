using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class PlctagNew
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public int? ArraySize { get; set; }
        public string DefaultValue { get; set; }
        public string Statistics { get; set; }
        public string Screensaver { get; set; }
        public string UnitCategory { get; set; }
        public long? UnitSubCategoryId { get; set; }
        public long? TextId { get; set; }
        public int? CycleTime { get; set; }

        public long Dbid { get; set; }
        public string TagName { get; set; }
        public string TagNumber { get; set; }
    }
}
