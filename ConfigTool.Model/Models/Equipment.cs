using ConfigTool.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class Equipment : IEntity<int>
    {
        public string IconPath { get; set; }
        public int Id { get; set; }
        public int? TagId { get; set; }
        public int? GroupTextId { get; set; }
        public int? TextId { get; set; }
        public short? ReadWriteTypeId { get; set; }
        public short? ValueTypeId { get; set; }
        public string Enable { get; set; }
        public string Visible { get; set; }

        public string Min { get; set; }
        public string Max { get; set; }
        public string Value { get; set; }
        public int? SortPosition { get; set; }
    }
}
