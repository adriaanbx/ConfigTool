using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ValueType = ConfigTool.Models.ValueType;

namespace ConfigTool.UI.Repositories
{
    public class ValueTypeRepository : GenericRepository<ValueType, ModelContext, short>, IValueTypeRepository
    {
        public ValueTypeRepository(ModelContext modelContext) : base(modelContext)
        {
        }
    }
}
