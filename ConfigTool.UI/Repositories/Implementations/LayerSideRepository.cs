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
    public class LayerSideRepository : GenericRepository<LayerSide, ModelContext, short, LayerSideWrapper>, ILayerSideRepository
    {
        public LayerSideRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<short>>> GetAllLookupAsync()
        {
            return await _context.LayerSide.OrderBy(p => p.Desc).Select(p =>
            new LookupItem<short>
            {
                Id = p.Id,
                DisplayMember = p.Desc
            }).ToListAsync();
        }
        public override async Task<IEnumerable<TableItem<LayerSide, short, LayerSideWrapper>>> GetTableLookupAsync()
        {
            return await _context.LayerSide.Select(p =>
            new LayerSideTableItem
            {
                Table = new Wrappers.LayerSideWrapper(p)
            }).ToListAsync();
        }
    }
}
