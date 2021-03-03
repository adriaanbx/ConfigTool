using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class PlcsamplingTags
    {
        public string SamplingType { get; set; }
        public string SamplingSort { get; set; }
        public int Id { get; set; }
        public int? RequestedNumberOfResultsTagId { get; set; }
        public int? AmountOfResultsTagId { get; set; }
        public int? AverageTagId { get; set; }
        public int? SigmaRelativeTagId { get; set; }
        public int? SigmaAbsoluteTagId { get; set; }
        public int? SigmaRelativeToleranceTagId { get; set; }
        public int? SigmaAbsoluteToleranceTagId { get; set; }
        public int? TolerancePlusPlusTagId { get; set; }
        public int? T2plusTagId { get; set; }
        public int? T1plusTagId { get; set; }
        public int? TolerancePlusTagId { get; set; }
        public int? MaximumTagId { get; set; }
        public int? NominalTagId { get; set; }
        public int? MinimumTagId { get; set; }
        public int? ToleranceMinTagId { get; set; }
        public int? T1minTagId { get; set; }
        public int? T2minTagId { get; set; }
        public int? ToleranceMinMinTagId { get; set; }
        public int? IsMeasuredTagId { get; set; }
        public int? ResultTagId { get; set; }
        public int? CorrectionTagId { get; set; }
        public int? CorrectionLimitTagId { get; set; }
        public int? AcceptedTabletsOutsideT1tagId { get; set; }
        public int? AmountOfTabletsOutsideT1tagId { get; set; }
        public int? AmountOfTabletsOutsideT2tagId { get; set; }
        public int? AvailableTagId { get; set; }
        public int? SamplingResultIdtagId { get; set; }
        public int? SamplingCycleAbortedTagId { get; set; }
        public int? FeederAngleTagId { get; set; }
        public int? FeederSpeedTagId { get; set; }
        public int? StarwheelSpeedTagId { get; set; }

    }
}
