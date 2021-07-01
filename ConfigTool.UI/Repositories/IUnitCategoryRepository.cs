﻿using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValueType = ConfigTool.Models.ValueType;

namespace ConfigTool.UI.Repositories
{
    public interface IUnitCategoryRepository : IGenericRepository<UnitCategory, int>
    {
    }
}
