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
    public class ToolingParameterRepository : GenericRepository<ToolingParameter, ModelContext, int, ToolingParameterWrapper>, IToolingParameterRepository
    {
        public ToolingParameterRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.ToolingParameter.OrderBy(e => e.SortPosition).Select(e =>
           new LookupItem<int>
           {
               Id = e.Id,
               DisplayMember = _context.TextLanguage.First(t => t.TextId == e.TextId).Text
           }).ToListAsync();
        }
              
        public override async Task<IEnumerable<TableItem<ToolingParameter, int, ToolingParameterWrapper>>> GetTableLookupAsync()
        {
            return await _context.ToolingParameter.Select(e =>
            new ToolingParameterTableItem
            {
                Table = new Wrappers.ToolingParameterWrapper(e),
                Plctag = e.Tag.Name,
                ValueType = e.ValueType.Name,
                ReadWriteType = e.ReadWriteType.Name,
                Text = _context.TextLanguage.First(t => t.TextId == e.TextId).Text,
                GroupText = _context.TextLanguage.First(t => t.TextId == e.GroupTextId).Text
            }).ToListAsync();
        }
    }
}
