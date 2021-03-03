using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Language
    {
        public string CultureCode { get; set; }
        public int TextId { get; set; }
        public int Id { get; set; }
        public string CultureIdentifier { get; set; }

    }
}
