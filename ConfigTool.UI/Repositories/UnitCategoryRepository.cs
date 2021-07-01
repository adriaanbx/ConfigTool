using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ConfigTool.UI.Repositories
{
    public class UnitCategoryRepository : GenericRepository<UnitCategory, ModelContext, int>, IUnitCategoryRepository
    {
        public UnitCategoryRepository(ModelContext modelContext) : base(modelContext)
        {
        }
    }
}
