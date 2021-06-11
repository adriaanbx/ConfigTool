using ConfigTool.DataAccess;
using ConfigTool.Models;
using ConfigTool.UI.Repositories;
using ConfigTool.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Diagnostics;
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
                Plctag = new Wrappers.PlctagWrapper(p),
                DataBlock = p.DataBlock.Name,
                //UnitCategory = p.UnitCategory.Name,
                ValueType = p.ValueType.Name
            }).ToListAsync();
        }

        public void RejectChanges()
        {
            //_dbcontext.ChangeTracker.Clear();


            var changedEntries = _dbcontext.ChangeTracker.Entries()
                   .Where(e => e.State != EntityState.Unchanged);

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;

                    // Where a Key Entry has been deleted, reloading from the source is required to ensure that the entity's relationships are restored (undeleted).
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        //TODO gebruiken voor de combobox selectie?
        public async Task<IEnumerable<LookupItem<int>>> GetDatablockLookupAsync()
        {
            return await _dbcontext.DataBlock.Select(p =>
            new LookupItem<int>
            {
                Id = p.Id,
                DisplayMember = p.Name
            }).ToListAsync();
        }


        public IEnumerable<IForeignKey> GetForeignKeys()
        {
            return _dbcontext.Model.FindEntityType(typeof(Plctag)).GetForeignKeys();
        }

        public async Task<IEnumerable<LookupItem<short>>> GetValueTypeLookupAsync()
        {
            return await _dbcontext.ValueType.Select(p =>
                new LookupItem<short>
                {
                    Id = p.Id,
                    DisplayMember = p.Name
                }).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            return _dbcontext.ChangeTracker.HasChanges();
        }
    }
}
