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
    public class LanguageRepository : GenericRepository<Language, ModelContext, int, LanguageWrapper>, ILanguageRepository
    {
        public LanguageRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.Language.OrderBy(p => p.CultureCode).Select(p =>
            new LookupItem<int>
             {
                 Id = p.Id,
                 DisplayMember = p.CultureCode
            }).ToListAsync();
        }
        public override async Task<IEnumerable<TableItem<Language, int, LanguageWrapper>>> GetTableLookupAsync()
        {
            return await _context.Language.Select(p =>
            new LanguageTableItem
            {
                Table = new Wrappers.LanguageWrapper(p),
                Text = p.Text.Id.ToString()              
            }).ToListAsync();
        }
    }
}
