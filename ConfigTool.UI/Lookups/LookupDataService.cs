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
    public class LookupDataService : IPlctagLookupDataService, IDatablockLookupDataService, IValueTypeLookupDataService, IUnitCategoryLookupDataService, ITextLanguageLookupDataService
    {
        private readonly ModelContext _dbcontext;

        public LookupDataService(ModelContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<NavigationItemPlctag>> GetPlctagLookupAsync()
        {
            return await _dbcontext.Plctag.Include(p => p.DataBlock).Include(p => p.UnitCategory).Select(p =>
            new NavigationItemPlctag
            {
                Plctag = new Wrappers.PlctagWrapper(p),
                DataBlock = p.DataBlock.Name,
                UnitCategory = p.UnitCategory.Name,
                ValueType = p.ValueType.Name,
                Text = _dbcontext.TextLanguage.First(t => t.TextId == p.TextId).Text
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
            return await _dbcontext.DataBlock.Select(d =>
            new LookupItem<int>
            {
                Id = d.Id,
                DisplayMember = d.Name
            }).ToListAsync();
        }


        public IEnumerable<IForeignKey> GetForeignKeys()
        {
            return _dbcontext.Model.FindEntityType(typeof(Plctag)).GetForeignKeys();
        }

        public async Task<IEnumerable<LookupItem<short>>> GetValueTypeLookupAsync()
        {
            return await _dbcontext.ValueType.Select(v =>
                new LookupItem<short>
                {
                    Id = v.Id,
                    DisplayMember = v.Name
                }).ToListAsync();
        }
        
        public async Task<IEnumerable<LookupItem<int>>> GetUnitCategoryLookupAsync()
        {
            return await _dbcontext.UnitCategory.Select(p =>
                new LookupItem<int>
                {
                    Id = p.Id,
                    DisplayMember = p.Name
                }).ToListAsync();
        }
        public async Task<IEnumerable<LookupItem<int>>> GetTextLanguageLookupAsync()
        {
            return await _dbcontext.TextLanguage.Where(tl => tl.LanguageId == 1 && (tl.TextId > 60000 && tl.TextId < 60100) || tl.TextId == 0 ).OrderBy(tl => tl.Text).Select(tl =>
                new LookupItem<int>
                {
                    Id = tl.TextId,
                    DisplayMember = tl.Text
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
