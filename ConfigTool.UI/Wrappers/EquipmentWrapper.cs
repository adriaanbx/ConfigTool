using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class EquipmentWrapper : ModelWrapper<Equipment, int>
    {
        public int? PlctagId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public int? TextId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public int? GroupTextId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public short? ReadWriteTypeId
        {
            get { return GetValue<short>(); }
            set { SetValue(value); }
        }
        public short? ValueTypeId
        {
            get { return GetValue<short>(); }
            set { SetValue(value); }
        }
        public string Enable
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Visible
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Min
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Max
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Value
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public int? SortPosition
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public EquipmentWrapper(Equipment model) : base(model)
        {
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Value):
                    if (string.Equals(Value, "Test", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Test is not a valid Engineeringtag";
                    }
                    break;
            }
        }
    }
}
