using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class LanguageWrapper : ModelWrapper<Language, int>
    {
        public string CultureCode
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }
        public int TextId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }


        public string CultureIdentifier
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        public bool Enabled
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
            }
        }


        public LanguageWrapper(Language model) : base(model)
        {
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(CultureCode):
                    if (string.Equals(CultureCode, "Test", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Test is not a valid CultureCode";
                    }
                    break;
            }
        }
    }
}
