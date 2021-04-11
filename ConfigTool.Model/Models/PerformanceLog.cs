using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class PerformanceLog
    {

        public int Id { get; set; }
        public int? TagId { get; set; }
        public string Visible { get; set; }
        public int? NumberOfDistributions { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
    }
}
