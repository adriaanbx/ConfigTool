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
    public class PlctagRepository : GenericRepository<Plctag, ModelContext, int, PlctagWrapper>, IPlctagRepository
    {
        public PlctagRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.Plctag.OrderBy(p => p.Name).Select(p =>
           new LookupItem<int>
           {
               Id = p.Id,
               DisplayMember = p.Name
           }).ToListAsync();
        }

        public async Task<IEnumerable<LookupItem<int>>> GetAllPressParametersLookupAsync()
        {
            return await _context.Plctag.Where(p => p.DataBlockId == 20).OrderBy(p => p.Name).Select(p =>
           new LookupItem<int>
           {
               Id = p.Id,
               DisplayMember = p.DataBlock.Name + "." + p.Name
           }).ToListAsync();
        }

        public async Task<IEnumerable<LookupItem<int>>> GetAllParametersLookupAsync()
        {
            return await _context.Plctag.Where(p => p.Name.Contains("PARXX")).OrderBy(p => p.Name).Select(p =>
           new LookupItem<int>
           {
               Id = p.Id,
               DisplayMember = p.DataBlock.Name + "." + p.Name
           }).ToListAsync();
        }

        public async Task<IEnumerable<LookupItem<int>>> GetAllPlcMappingLookupAsync()
        {
            return await _context.Plctag.Where(p => p.DataBlock.Name.Contains("MAPXX")).OrderBy(p => p.Name).Select(p =>
           new LookupItem<int>
           {
               Id = p.Id,
               DisplayMember = p.DataBlock.Name + "." + p.Name
           }).ToListAsync();
        }

        public override async Task<IEnumerable<TableItem<Plctag, int, PlctagWrapper>>> GetTableLookupAsync()
        {
            return await _context.Plctag.Include(p => p.DataBlock).Include(p => p.UnitCategory).Select(p =>
            new PlctagTableItem
            {
                Table = new Wrappers.PlctagWrapper(p),
                DataBlock = p.DataBlock.Name,
                UnitCategory = p.UnitCategory.Name,
                ValueType = p.ValueType.Name,
                Text = _context.TextLanguage.First(t => t.TextId == p.TextId).Desc
            }).ToListAsync();
        }
    }
}
