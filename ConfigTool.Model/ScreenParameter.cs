using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class ScreenParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public int Id { get; set; }
        public int? TagId { get; set; }
    }
}
