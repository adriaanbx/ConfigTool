using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class PressParameter
    {
        public string Name { get; set; }
        public short? LayerSideId { get; set; }
        public int Id { get; set; }
        public int? TagId { get; set; }
        public int? PressParameterTypeId { get; set; }

    }
}
