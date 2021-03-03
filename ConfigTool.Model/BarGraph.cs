using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class BarGraph
    {
        public int Id { get; set; }
        public int? NumberOfPunchesTagId { get; set; }
        public int? EnabledPunchesTagId { get; set; }
        public int? ActualTagId { get; set; }
        public int? MinTagId { get; set; }
        public int? MaxTagId { get; set; }
        public int? LowerDeviationTagId { get; set; }
        public int? UpperDeviationTagId { get; set; }
        public int? RejectMinTagId { get; set; }
        public int? RejectPlusTagId { get; set; }
        public int? CorrectionMinTagId { get; set; }
        public int? CorrectionPlusTagId { get; set; }
        public int? NominalTagId { get; set; }
        public int? AverageTagId { get; set; }
        public int? TextId { get; set; }
        public string Visible { get; set; }

    }
}
