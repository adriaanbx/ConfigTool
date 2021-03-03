using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class TurretProcedure
    {
        public int Id { get; set; }
        public int? TextId { get; set; }
        public int? GroupTextId { get; set; }

        public string Enable { get; set; }
        public string Visible { get; set; }
    }
}
