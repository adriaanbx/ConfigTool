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
    public class EngineeringRepository : GenericRepository<Engineering, ModelContext, int, EngineeringWrapper>, IEngineeringRepository
    {
        public EngineeringRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.Engineering.OrderBy(e => e.SortPosition).Select(e =>
           new LookupItem<int>
           {
               Id = e.Id,
               DisplayMember = _context.TextLanguage.First(t => t.TextId == e.TextId).Text
           }).ToListAsync();
        }
              
        public override async Task<IEnumerable<TableItem<Engineering, int, EngineeringWrapper>>> GetTableLookupAsync()
        {
            return await _context.Engineering.Select(e =>
            new EngineeringTableItem
            {
                Table = new Wrappers.EngineeringWrapper(e),
                Plctag = e.Plctag.Name,
                ValueType = e.ValueType.Name,
                ReadWriteType = e.ReadWriteType.Name,
                Text = _context.TextLanguage.First(t => t.TextId == e.TextId).Text,
                GroupText = _context.TextLanguage.First(t => t.TextId == e.GroupTextId).Text
            }).ToListAsync();
        }
    }
}
