using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class LayerSideWrapper : ModelWrapper<LayerSide, short>
    {
        public string Desc
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

       
        public LayerSideWrapper(LayerSide model) : base(model)
        {
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Desc):
                    if (string.Equals(Desc, "Test", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Test is not a valid Plctag";
                    }
                    break;
            }
        }
    }
}
