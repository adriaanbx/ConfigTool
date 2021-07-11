using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class PlctagWrapper : ModelWrapper<Plctag, int>
    {

        public int? ArraySize
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public string DefaultValue
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Statistics
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Screensaver
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public int? CycleTime
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Number
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }
        public int Id
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public int? TextId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public int DataBlockId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public short ValueTypeId
        {
            get { return GetValue<short>(); }
            set { SetValue(value); }
        }
        public int? UnitCategoryId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public bool Log
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public PlctagWrapper(Plctag model) : base(model)
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
