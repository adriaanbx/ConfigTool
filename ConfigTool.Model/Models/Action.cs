using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Action
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int? TextId { get; set; }
        public int? GroupTextId { get; set; }
        public int? MessageId { get; set; }
        public string Visible { get; set; }

    }
}
