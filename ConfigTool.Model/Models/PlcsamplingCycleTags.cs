using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class PlcsamplingCycleTags
    {
        public int Id { get; set; }
        public int? SamplingResultNumberTagId { get; set; }
        public int SamplingCycleAbortedTagId { get; set; }
        public int FeederAngleTagId { get; set; }
        public int? FeederSpeedTagId { get; set; }
        public int? StarWheelSpeedTagId { get; set; }
        public int SamplingOutputId { get; set; }

    }
}
