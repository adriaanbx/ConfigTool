using ConfigTool.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class PressParameter : IEntity<int>
    {
        public string Name { get; set; }
        public short? LayerSideId { get; set; }
        public int Id { get; set; }
        public int? PlctagId { get; set; }
        public int? PressParameterTypeId { get; set; }

    }
}
