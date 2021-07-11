using ConfigTool.DataAccess;
using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
using ConfigTool.UI.Wrappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ConfigTool.UI.Repositories
{
    public class UnitCategoryRepository : GenericRepository<UnitCategory, ModelContext, int, UnitCategoryWrapper>, IUnitCategoryRepository
    {
        public UnitCategoryRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.UnitCategory.OrderBy(u => u.Name).Select(p =>
                new LookupItem<int>
                {
                    Id = p.Id,
                    DisplayMember = p.Name
                }).ToListAsync();
        }

        public override Task<IEnumerable<TableItem<UnitCategory, int, UnitCategoryWrapper>>> GetTableLookupAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
