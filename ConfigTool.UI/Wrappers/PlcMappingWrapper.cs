using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class PlcMappingWrapper : ModelWrapper<Plcmapping, int>
    {
        public int? TagId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string ElDesingCode
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public short? LayerSideId
        {
            get { return GetValue<short>(); }
            set { SetValue(value); }
        }
        public double? Value
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public PlcMappingWrapper(Plcmapping model) : base(model)
        {
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Name):
                    if (string.Equals(Name, "Test", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Test is not a valid PlcMappingTag";
                    }
                    break;
            }
        }
    }
}
