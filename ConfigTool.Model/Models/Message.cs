using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Message
    {
        public string Category { get; set; }
        public int Id { get; set; }
        public int? LogTextId { get; set; }
        public int? DisplayTextId { get; set; }
        public int? LogCategoryTextId { get; set; }
        public int? LogSubCategoryTextId { get; set; }

    }
}
