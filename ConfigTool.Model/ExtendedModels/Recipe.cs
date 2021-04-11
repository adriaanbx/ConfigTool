using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Recipe
    {
 
        public List<Batch> Batches { get; set; }

        public List<RecipeParameterValue> RecipeParameterValues { get; set; }

        public Status Status { get; set; }
    }
}
