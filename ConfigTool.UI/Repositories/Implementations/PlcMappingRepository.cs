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
    public class PlcMappingRepository : GenericRepository<Plcmapping, ModelContext, int,PlcMappingWrapper>, IPlcMappingRepository
    {
        public PlcMappingRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.Plcmapping.OrderBy(p => p.Name).Select(p =>
           new LookupItem<int>
           {
               Id = p.Id,
               DisplayMember = p.Name
           }).ToListAsync();
        }
              
        public override async Task<IEnumerable<TableItem<Plcmapping, int, PlcMappingWrapper>>> GetTableLookupAsync()
        {
            return await _context.Plcmapping.Select(p =>
            new PlcMappingTableItem
            {
                Table = new Wrappers.PlcMappingWrapper(p),
                Plctag = p.Tag.Name
            }).ToListAsync();
        }
    }
}
