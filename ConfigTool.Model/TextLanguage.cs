using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class TextLanguage
    {
        public int Id { get; set; }
        public int TextId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
    }
}
