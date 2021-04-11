using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class EcmparameterValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Format { get; set; }
        public int Ecmid { get; set; }
        public int EcmparameterId { get; set; }
        public int? UnitId { get; set; }
    }
}
