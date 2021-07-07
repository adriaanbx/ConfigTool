using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class DatablockRepository : GenericRepository<DataBlock, ModelContext, int>, IDatablockRepository
    {
        public DatablockRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public override async Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.DataBlock.OrderBy(d => d.Name).Select(d =>
             new LookupItem<int>
             {
                 Id = d.Id,
                 DisplayMember = d.Name
             }).ToListAsync();
        }
    }
}
