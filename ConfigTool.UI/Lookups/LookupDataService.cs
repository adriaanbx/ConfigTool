using ConfigTool.DataAccess;
using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigTool.UI.Lookups
{
    public class LookupDataService : IPlctagLookupDataService, IDatablockLookupDataService, IValueTypeLookupDataService
    {
        private readonly ModelContext _dbcontext;

        public LookupDataService(ModelContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<NavigationItemPlctag>> GetPlctagLookupAsync()
        {
            return await _dbcontext.Plctag.Include(p => p.DataBlock).Select(p =>
            new NavigationItemPlctag
            {
                //TODO verder aanvullen met rest van kolommen
                //Plctag = p,
                Id = p.Id,
                Name = p.Name,
                ArraySize = p.ArraySize,
                Number = p.Number,
                DataBlock = p.DataBlock.Name,
                DataBlockId = p.DataBlockId,
                ValueTypeId = p.ValueTypeId,
                //UnitCategory = p.UnitCategory.Name,
                ValueType = p.ValueType.Name
            }).ToListAsync();
        }

        //TODO gebruiken voor de combobox selectie?
        public async Task<IEnumerable<LookupItem>> GetDatablockLookupAsync()
        {
            return await _dbcontext.DataBlock.Select(p =>
            new LookupItem
            {
                Id = p.Id,
                DisplayMember = p.Name
            }).ToListAsync();
        }


        public IEnumerable<IForeignKey> GetForeignKeys()
        {
            return _dbcontext.Model.FindEntityType(typeof(Plctag)).GetForeignKeys();
        }

        public Task<IEnumerable<LookupItem>> GetValueTypeLookupAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
