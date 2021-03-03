using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class ExpressionsReference
    {
        public long Id { get; set; }
        public string Table { get; set; }
        public string Column { get; set; }
        public string ValueColumn { get; set; }
    }
}
