using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConfigTool.Models
{
    public partial class Tooling
    {

        public ObservableCollection<ToolingParameterValue> ToolingParameterValues { get; set; }

        public bool Started { get; set; }


        public List<Batch> Batches { get; set; }

        public Status Status { get; set; }
    }
}
