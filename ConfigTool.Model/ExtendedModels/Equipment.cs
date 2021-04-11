using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Equipment
    {

        public Plctag Tag { get; set; }

        public Text Text { get; set; }

        public ValueType ValueType { get; set; }

        public ReadWriteType ReadWriteType { get; set; }
    }
}
