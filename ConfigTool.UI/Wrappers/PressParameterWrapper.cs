using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class PressParameterWrapper : ModelWrapper<PressParameter, int>
    {
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public short? LayerSideId
        {
            get { return GetValue<short>(); }
            set { SetValue(value); }
        }

        public int? PlctagId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int? PressParameterTypeId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public bool Value
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public PressParameterWrapper(PressParameter model) : base(model)
        {
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Name):
                    if (string.Equals(Name, "Test", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Test is not a valid Plctag";
                    }
                    break;
            }
        }
    }
}
