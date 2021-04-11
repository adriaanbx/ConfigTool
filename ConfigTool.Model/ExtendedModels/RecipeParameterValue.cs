using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class RecipeParameterValue
    {
        public RecipeParameter RecipeParameter { get; set; }


        public Recipe Recipe { get; set; }

    
    }
}
