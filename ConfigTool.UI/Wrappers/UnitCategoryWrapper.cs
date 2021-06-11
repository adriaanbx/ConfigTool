using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class UnitCategoryWrapper : ModelWrapper<UnitCategory>
    {
        public int Id
        {
            get { return GetValue<int>(); }
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

        public string PrimaryFormat
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }
        public string SecondaryFormat
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }
        public int? TextId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public int? PrimaryUnitId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public int? SecondaryUnitId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public int? ActiveUnitId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public short? SubCategoryNumber
        {
            get { return GetValue<short>(); }
            set
            {
                SetValue(value);
            }
        }
        public int? PrimaryUnitActiveTagId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }

        public int? UnitConversionFactorTagId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }


        public UnitCategoryWrapper(UnitCategory model) : base(model)
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
