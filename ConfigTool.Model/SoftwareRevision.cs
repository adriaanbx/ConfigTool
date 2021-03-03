using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class SoftwareRevision
    {
        public int Id { get; set; }
        public string Hmiversion { get; set; }
        public string Plcversion { get; set; }

    }
}
