using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class CalibrationParameterValue
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public string Value { get; set; }
        public short? CalibrationParameterTypeId { get; set; }
        public int? TagId { get; set; }
        public int? CalibrationParameterId { get; set; }

    }
}
