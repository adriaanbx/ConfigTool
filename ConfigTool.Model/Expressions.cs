using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Expressions
    {
        public long Id { get; set; }
        public long IdStart { get; set; }
        public string Value { get; set; }
        public long? PlcTagId { get; set; }
        public long? EngineeringId { get; set; }
        public long? EquipmentId { get; set; }
        public long? RecipeId { get; set; }
        public long? EcmId { get; set; }
        public long? ScreenId { get; set; }
        public long? PressId { get; set; }
        public long? ToolId { get; set; }
        public long? Sort { get; set; }
        public string Tabledest { get; set; }
        public string Columndest { get; set; }
    }
}
