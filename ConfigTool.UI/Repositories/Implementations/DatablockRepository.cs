using ConfigTool.DataAccess;
using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
using ConfigTool.UI.ViewModels.TableViewModels;
using ConfigTool.UI.Wrappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class DatablockRepository : GenericRepository<DataBlock, ModelContext, int,DatablockWrapper>, IDatablockRepository
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

        public override async Task<IEnumerable<TableItem<DataBlock, int, DatablockWrapper>>> GetTableLookupAsync()
        {
            return await _context.DataBlock.Select(d =>
            new DatablockTableItem
            {
                Table = new Wrappers.DatablockWrapper(d),
             
            }).ToListAsync();
        }
    }
}
