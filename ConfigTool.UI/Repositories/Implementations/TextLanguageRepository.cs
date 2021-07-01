using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class TextLanguageRepository : GenericRepository<TextLanguage, ModelContext, int>, ITextLanguageRepository
    {

        public TextLanguageRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async Task<TextLanguage> GetByTextIdAsync(int textId)
        {
            return await _context.TextLanguage.FirstOrDefaultAsync(p => p.TextId == textId);
        }
    }
}
