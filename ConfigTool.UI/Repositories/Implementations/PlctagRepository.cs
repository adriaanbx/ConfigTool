using ConfigTool.DataAccess;
using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class PlctagRepository : GenericRepository<Plctag, ModelContext, int>, IPlctagRepository
    {
        public PlctagRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<TableItemPlctag>> GetPlctagLookupAsync()
        {
            return await _context.Plctag.Include(p => p.DataBlock).Include(p => p.UnitCategory).Select(p =>
            new TableItemPlctag
            {
                Plctag = new Wrappers.PlctagWrapper(p),
                DataBlock = p.DataBlock.Name,
                UnitCategory = p.UnitCategory.Name,
                ValueType = p.ValueType.Name,
                Text = _context.TextLanguage.First(t => t.TextId == p.TextId).Text
            }).ToListAsync();
        }
    }
}
