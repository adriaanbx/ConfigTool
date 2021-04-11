using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class SubReportValue
    {
        public string Name { get; set; }

        public int Id { get; set; }
        public int? SubReportId { get; set; }
    }
}
