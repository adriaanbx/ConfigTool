using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class OpcserverTag
    {
        public string Description { get; set; }
        public string GroupName { get; set; }
        public int Id { get; set; }
        public int? TagId { get; set; }
        public string Visible { get; set; }

    }
}
