using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class EngineeringWrapper : ModelWrapper<Engineering, int>
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


        //public Text Text
        //{
        //    get { return GetValue<Text>(); }
        //    set { SetValue(value); }
        //}
        //public Plctag Plctag
        //{
        //    get { return GetValue<Plctag>(); }
        //    set { SetValue(value); }
        //}
        //public Models.ValueType ValueType
        //{
        //    get { return GetValue<Models.ValueType>(); }
        //    set { SetValue(value); }
        //}
        //public ReadWriteType ReadWriteType
        //{
        //    get { return GetValue<ReadWriteType>(); }
        //    set { SetValue(value); }
        //}


        public EngineeringWrapper(Engineering model) : base(model)
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
