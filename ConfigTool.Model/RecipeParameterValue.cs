using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class RecipeParameterValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Format { get; set; }
        public int RecipeId { get; set; }
        public int RecipeParameterId { get; set; }
        public int? UnitId { get; set; }
    }
}
