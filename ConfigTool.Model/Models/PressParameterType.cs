using ConfigTool.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class PressParameterType : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
