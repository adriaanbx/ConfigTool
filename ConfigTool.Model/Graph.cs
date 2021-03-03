using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Graph
    {
        public string Name { get; set; }
        public string BatchName { get; set; }
        public string BatchLot { get; set; }
        public long? BatchStartDateTime { get; set; }
        public int Id { get; set; }

    }
}
