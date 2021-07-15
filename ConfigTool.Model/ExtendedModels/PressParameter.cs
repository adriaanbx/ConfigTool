using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class PressParameter
    {
        public bool Value { get; set; }

        public Plctag Tag { get; set; }
        public LayerSide LayerSide { get; set; }
        public PressParameterType PressParameterType { get; set; }

    }
}
