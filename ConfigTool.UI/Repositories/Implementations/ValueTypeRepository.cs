using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValueType = ConfigTool.Models.ValueType;

namespace ConfigTool.UI.Repositories
{
    public class ValueTypeRepository : GenericRepository<ValueType, ModelContext, short>, IValueTypeRepository
    {
        public ValueTypeRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<short>>> GetAllLookupAsync()
        {
            return await _context.ValueType.OrderBy(v => v.Name).Select(v =>
                 new LookupItem<short>
                 {
                     Id = v.Id,
                     DisplayMember = v.Name
                 }).ToListAsync();
        }
    }
}
