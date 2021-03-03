using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Procedure
    {
        public string Path { get; set; }
        public int Id { get; set; }
        public int? TextId { get; set; }
        public int? GroupTextId { get; set; }

    }
}
