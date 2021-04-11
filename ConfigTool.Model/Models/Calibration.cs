using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Calibration
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? TextId { get; set; }
        public string Visible { get; set; }
        public string Dependency { get; set; }
        public long? VisibleExp { get; set; }
    }
}
