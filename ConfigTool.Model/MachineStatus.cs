using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class MachineStatus
    {
        public string Color { get; set; }
        public string Enable { get; set; }
        public int Id { get; set; }
        public int? ActiveTagId { get; set; }
        public int? SetActiveTagId { get; set; }
        public int? SetInActiveTagId { get; set; }
        public int? TextId { get; set; }
        public string Visible { get; set; }

    }
}
