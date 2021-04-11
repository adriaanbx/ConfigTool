using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigTool.Models
{
    public partial class RecipeParameter
    {
        public ValueType ValueType { get; set; }
        public ReadWriteType ReadWriteType { get; set; }

        [Column("TagId")]
        public Plctag Tag { get; set; }
    }
}
