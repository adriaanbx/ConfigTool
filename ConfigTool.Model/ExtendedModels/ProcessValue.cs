using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class ProcessValue
    {
        public Plctag Tag { get; set; }
       
        public bool ShowOnBatchOverview { get; set; }
    }
}
