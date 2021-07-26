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
    public class EquipmentRepository : GenericRepository<Equipment, ModelContext, int, EquipmentWrapper>, IEquipmentRepository
    {
        public EquipmentRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.Engineering.OrderBy(e => e.SortPosition).Select(e =>
           new LookupItem<int>
           {
               Id = e.Id,
               DisplayMember = _context.TextLanguage.First(t => t.TextId == e.TextId).Desc
           }).ToListAsync();
        }
              
        public override async Task<IEnumerable<TableItem<Equipment, int, EquipmentWrapper>>> GetTableLookupAsync()
        {
            return await _context.Equipment.Select(e =>
            new EquipmentTableItem
            {
                Table = new Wrappers.EquipmentWrapper(e),
                Plctag = e.Plctag.Name,
                ValueType = e.ValueType.Name,
                ReadWriteType = e.ReadWriteType.Name,
                Text = _context.TextLanguage.First(t => t.TextId == e.TextId).Desc,
                GroupText = _context.TextLanguage.First(t => t.TextId == e.GroupTextId).Desc
            }).ToListAsync();
        }
    }
}
