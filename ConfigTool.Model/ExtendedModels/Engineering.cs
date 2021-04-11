using System;
using System.Collections.Generic;
using ConfigTool.Models;

namespace ConfigTool.Models
{
    public partial class Engineering
    {      
        
        public Text Text { get; set; }

        public Plctag Tag { get; set; }

        public ValueType ValueType { get; set; }

        public ReadWriteType ReadWriteType { get; set; }

   
    }
}
