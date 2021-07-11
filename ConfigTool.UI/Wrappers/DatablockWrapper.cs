using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class DatablockWrapper : ModelWrapper<DataBlock, int>
    {
    
        public string Name
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        public string Number
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        public bool Enable
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
            }
        }

        public DatablockWrapper(DataBlock model) : base(model)
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
