using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class ReportSubReport
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public int SubReportId { get; set; }
      }
}
