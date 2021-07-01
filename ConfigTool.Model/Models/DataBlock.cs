using ConfigTool.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class DataBlock : IEntity<int>
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int Id { get; set; }

    }
}
