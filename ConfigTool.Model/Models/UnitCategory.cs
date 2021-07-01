using ConfigTool.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class UnitCategory : IEntity<int>
    {
        public string Name { get; set; }
        public string PrimaryFormat { get; set; }
        public string SecondaryFormat { get; set; }
        public int Id { get; set; }
        public int? TextId { get; set; }
        public int? PrimaryUnitId { get; set; }
        public int? SecondaryUnitId { get; set; }
        public int? ActiveUnitId { get; set; }
        public short? SubCategoryNumber { get; set; }

        public int? PrimaryUnitActiveTagId { get; set; }
        public int? UnitConversionFactorTagId { get; set; }
    }
}
