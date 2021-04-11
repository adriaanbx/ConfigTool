using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class TurretProcedureStep
    {
        public string Path { get; set; }
        public int Id { get; set; }
        public int? TurretProcedureId { get; set; }
        public int? Number { get; set; }

    }
}
