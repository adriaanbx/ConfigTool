using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class CsvlogFile
    {
        public string CsvfileName { get; set; }
        public string NewFile { get; set; }
        public int Id { get; set; }
        public int? TagId { get; set; }
        public int? LogTriggerTagId { get; set; }
        public int? ColumnTextId { get; set; }
        public string Enable { get; set; }

    }
}
