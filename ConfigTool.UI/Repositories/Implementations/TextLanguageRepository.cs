using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class TextLanguageRepository : GenericRepository<TextLanguage, ModelContext, int>, ITextLanguageRepository
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
    }
}
