using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigTool.Models
{
    public partial class Plctag
    {
        public DataBlock DataBlock { get; set; }

        [Column("UnitCategoryId")]
        public UnitCategory UnitCategory { get; set; }

        public ValueType ValueType { get; set; }

        public bool Log { get; set; }


    }
}
