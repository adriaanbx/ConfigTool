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
    public class PressParameterRepository : GenericRepository<PressParameter, ModelContext, int, PressParameterWrapper>, IPressParameterRepository
    {
        public PressParameterRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.PressParameter.OrderBy(p => p.Name).Select(p =>
            new LookupItem<int>
             {
                 Id = p.Id,
                 DisplayMember = p.Name
             }).ToListAsync();
        }
        public override async Task<IEnumerable<TableItem<PressParameter, int, PressParameterWrapper>>> GetTableLookupAsync()
        {
            return await _context.PressParameter.Include(p => p.Plctag).Include(p => p.LayerSide).Include(p => p.PressParameterType).Select(p =>
            new PressParameterTableItem
            {
                Table = new Wrappers.PressParameterWrapper(p),
                Plctag = p.Plctag.Name,
                LayerSide = p.LayerSide.Desc,
                PressParameterType = p.PressParameterType.Name
            }).ToListAsync();
        }
    }
}
