using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigTool.Models
{
    public partial class UnitCategory
    {
        [Column("ActiveUnitID")]
        public Unit ActiveUnit { get; set; }
        [Column("PrimaryUnitID")]
        public Unit PrimaryUnit { get; set; }
        [Column("SecondaryUnitID")]
        public Unit SecondaryUnit { get; set; }

    
    }
}
