using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ValueType = ConfigTool.Models.ValueType;

namespace ConfigTool.UI.Wrappers
{
    public class ValueTypeWrapper : ModelWrapper<ValueType>
    {
        public short Id
        {
            get { return GetValue<short>(); }
            set { SetValue(value); }
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        //public string Number
        //{
        //    get { return GetValue<string>(); }
        //    set
        //    {
        //        SetValue(value);
        //    }
        //}

        public ValueTypeWrapper(ValueType model) : base(model)
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
