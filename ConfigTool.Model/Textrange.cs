using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Textrange
    {
        public long Id { get; set; }
        public string Table { get; set; }
        public long? Range { get; set; }
        public string Column { get; set; }
    }
}
