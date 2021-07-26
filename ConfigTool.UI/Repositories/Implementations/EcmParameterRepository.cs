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
    public class EcmParameterRepository : GenericRepository<Ecmparameter, ModelContext, int, EcmParameterWrapper>, IEcmParameterRepository
    {
        public EcmParameterRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.Ecmparameter.OrderBy(e => e.SortPosition).Select(e =>
           new LookupItem<int>
           {
               Id = e.Id,
               DisplayMember = _context.TextLanguage.First(t => t.TextId == e.TextId).Desc
           }).ToListAsync();
        }
              
        public override async Task<IEnumerable<TableItem<Ecmparameter, int, EcmParameterWrapper>>> GetTableLookupAsync()
        {
            return await _context.Ecmparameter.Select(e =>
            new EcmParameterTableItem
            {
                Table = new Wrappers.EcmParameterWrapper(e),
                Plctag = e.Tag.Name,
                ValueType = e.ValueType.Name,
                ReadWriteType = e.ReadWriteType.Name,
                Text = _context.TextLanguage.First(t => t.TextId == e.TextId).Desc,
                GroupText = _context.TextLanguage.First(t => t.TextId == e.GroupTextId).Desc
            }).ToListAsync();
        }
    }
}
