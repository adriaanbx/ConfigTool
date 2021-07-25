using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigTool.Models
{
    public partial class Plcmapping
    {
        public Plctag Tag { get; set; }

        public LayerSide LayerSide { get; set; }

    }
}
