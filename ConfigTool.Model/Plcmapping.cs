using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Plcmapping
    {
        public string Name { get; set; }
        public double? Value { get; set; }
        public string ElDesingCode { get; set; }
        public short? LayerSideId { get; set; }
        public int Id { get; set; }
        public int? TagId { get; set; }

    }
}
