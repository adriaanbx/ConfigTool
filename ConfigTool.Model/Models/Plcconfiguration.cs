using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Plcconfiguration
    {
        public string Plctype { get; set; }
        public string Ip { get; set; }
        public int Id { get; set; }
        public int? Port { get; set; }
        public int? Rack { get; set; }
        public int? Slot { get; set; }

    }
}
