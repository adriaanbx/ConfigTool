using ConfigTool.DataAccess;
using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
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
            return await _context.TextLanguage.Where(tl => tl.LanguageId == 1).OrderBy(tl => tl.Text).Select(tl =>
                new LookupItem<int>
                {
                    Id = tl.TextId,
                    DisplayMember = tl.Text
                }).ToListAsync();
        }

        public async Task<TextLanguage> GetByTextIdAsync(int textId)
        {
            return await _context.TextLanguage.FirstOrDefaultAsync(p => p.TextId == textId);
        }

        public override Task<IEnumerable<TableItem<TextLanguage, int, TextLanguageWrapper>>> GetTableLookupAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
