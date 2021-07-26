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
    public class TextLanguageRepository : GenericRepository<TextLanguage, ModelContext, int, TextLanguageWrapper>, ITextLanguageRepository
    {

        public TextLanguageRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.TextLanguage.Where(tl => tl.LanguageId == 1).OrderBy(tl => tl.Desc).Select(tl =>
                new LookupItem<int>
                {
                    Id = tl.TextId,
                    DisplayMember = tl.Desc
                }).ToListAsync();
        }

        public async Task<TextLanguage> GetByTextIdAsync(int textId)
        {
            return await _context.TextLanguage.FirstOrDefaultAsync(p => p.TextId == textId);
        }

        public async override Task<IEnumerable<TableItem<TextLanguage, int, TextLanguageWrapper>>> GetTableLookupAsync()
        {
            return await _context.TextLanguage.Select(p =>
            new TextLanguageTableItem
            {
                Table = new Wrappers.TextLanguageWrapper(p),
                Text = p.Desc,
                Language = p.Language.CultureCode
            }).ToListAsync();
        }
    }
}
