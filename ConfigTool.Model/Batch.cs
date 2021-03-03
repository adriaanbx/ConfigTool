using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Batch
    {
        public string Name { get; set; }
        public string LotNumber { get; set; }
        public string Comment { get; set; }
        public long CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public double Weight { get; set; }
        public long StartDateTime { get; set; }
        public long EndDateTime { get; set; }
        public int RecipeId { get; set; }
        public int? Ecmid { get; set; }
        public int? ToolingId { get; set; }

        public int Id { get; set; }
        public short BatchStatusId { get; set; }
    }
}
