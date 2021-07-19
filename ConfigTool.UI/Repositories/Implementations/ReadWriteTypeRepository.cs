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
    public class ReadWriteTypeRepository : GenericRepository<ReadWriteType, ModelContext, short, ReadWriteTypeWrapper>, IReadWriteTypeRepository
    {
        public ReadWriteTypeRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public override async Task<IEnumerable<LookupItem<short>>> GetAllLookupAsync()
        {
            return await _context.ReadWriteType.OrderBy(r => r.Name).Select(r =>
             new LookupItem<short>
             {
                 Id = r.Id,
                 DisplayMember = r.Name
             }).ToListAsync();
        }

        public override async Task<IEnumerable<TableItem<ReadWriteType, short, ReadWriteTypeWrapper>>> GetTableLookupAsync()
        {
            return await _context.ReadWriteType.Select(r =>
            new ReadWriteTypeTableItem
            {
                Table = new Wrappers.ReadWriteTypeWrapper(r),

            }).ToListAsync();
        }
    }
}
