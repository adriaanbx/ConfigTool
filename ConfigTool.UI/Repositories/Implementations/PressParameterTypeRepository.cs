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
    public class PressParameterTypeRepository : GenericRepository<PressParameterType, ModelContext, int, PressParameterTypeWrapper>, IPressParameterTypeRepository
    {
        public PressParameterTypeRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.PressParameterType.OrderBy(p => p.Name).Select(p =>
            new LookupItem<int>
             {
                 Id = p.Id,
                 DisplayMember = p.Name
             }).ToListAsync();
        }
        public override async Task<IEnumerable<TableItem<PressParameterType, int, PressParameterTypeWrapper>>> GetTableLookupAsync()
        {
            return await _context.PressParameterType.Select(p =>
            new PressParameterTypeTableItem
            {
                Table = new Wrappers.PressParameterTypeWrapper(p)              
            }).ToListAsync();
        }
    }
}
