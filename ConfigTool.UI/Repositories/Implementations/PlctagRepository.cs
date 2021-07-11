using ConfigTool.DataAccess;
using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
using ConfigTool.UI.ViewModels.TableViewModels;
using ConfigTool.UI.Wrappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class PlctagRepository : GenericRepository<Plctag, ModelContext, int, PlctagWrapper>, IPlctagRepository
    {
        public PlctagRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            throw new NotImplementedException();
        }
        public override async Task<IEnumerable<TableItem<Plctag, int, PlctagWrapper>>> GetTableLookupAsync()
        {
            return await _context.Plctag.Include(p => p.DataBlock).Include(p => p.UnitCategory).Select(p =>
            new TableItemPlctag
            {
                Table = new Wrappers.PlctagWrapper(p),
                DataBlock = p.DataBlock.Name,
                UnitCategory = p.UnitCategory.Name,
                ValueType = p.ValueType.Name,
                Text = _context.TextLanguage.First(t => t.TextId == p.TextId).Text
            }).ToListAsync();
        }
    }
}
