using ConfigTool.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class TextLanguage : IEntity<int>
    {
        public int Id { get; set; }
        public int TextId { get; set; }
        public int LanguageId { get; set; }
        public string Desc { get; set; }
    }
}
