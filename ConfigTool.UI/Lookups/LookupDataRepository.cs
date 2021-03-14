using ConfigTool.DataAccess;
using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigTool.UI.Lookups
{
    public class LookupDataRepository : IPlctagLookupDataRepository
    {
        private readonly ModelContext _dbcontext;

        public LookupDataRepository(ModelContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<LookupItem>> GetPlctagLookupAsync()
        {
            return await _dbcontext.Plctag.Select(p =>
            new LookupItem
            {
                Id = p.Id,
                DisplayMember = p.Name
            }).ToListAsync();
        }
    }
}
