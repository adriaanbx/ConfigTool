﻿using ConfigTool.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class ValueType : IEntity<short>
    {
        public short Id { get; set; }
        public string Name { get; set; }

    }
}
