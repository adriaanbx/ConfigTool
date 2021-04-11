using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Control
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public string EnableCondition { get; set; }
        public int? TagId { get; set; }
        public int? TextId { get; set; }
        public string VisibleCondition { get; set; }

        public int? ActionId { get; set; }
    }
}
