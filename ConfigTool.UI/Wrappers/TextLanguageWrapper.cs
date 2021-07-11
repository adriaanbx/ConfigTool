using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class TextLanguageWrapper : ModelWrapper<TextLanguage, int>
    {
        public int TextId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }

        public int LanguageId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }

        public string Text
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }


        public TextLanguageWrapper(TextLanguage model) : base(model)
        {
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Text):
                    if (string.Equals(Text, "Test", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Test is not a valid Text";
                    }
                    break;
            }
        }
    }
}
