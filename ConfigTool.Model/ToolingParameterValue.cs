using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class ToolingParameterValue
    {
        public int Id { get; set; }
        public string Format { get; set; }
        public int ToolingId { get; set; }
        public int? UnitId { get; set; }
        public int ToolingParameterId { get; set; }
        public string Value { get; set; }
    }
}
