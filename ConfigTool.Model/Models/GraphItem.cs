using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class GraphItem
    {
        public int ItemNumber { get; set; }
        public string Axis { get; set; }
        public string Color { get; set; }
        public int Id { get; set; }
        public int? GraphId { get; set; }
        public int? TagName { get; set; }

    }
}
