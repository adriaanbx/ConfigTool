using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigTool.Models
{
    public partial class Text
    {

        public IList<TextLanguage> TextLanguages { get; set; }

        //public string CurrentDisplayLanguage {
        //    get {
        //        //TODO: apply variable on LanguageID
        //        return TextLanguages.Where((listedTextLanguage) => listedTextLanguage.LanguageId == 1).FirstOrDefault().Text;
        //    }
        //    set { 
            
        //    }
        //}
    }
}
