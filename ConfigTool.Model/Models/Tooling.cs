using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Tooling
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string SerialNumber { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string Comment { get; set; }
        public long? CreationDate { get; set; }
        public short StatusId { get; set; }
    }
}
