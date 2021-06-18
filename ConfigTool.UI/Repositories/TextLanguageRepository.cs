using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class TextLanguageRepository : ITextLanguageRepository
    {
        private readonly ModelContext _modelContext;

        public TextLanguageRepository(ModelContext modelContext)
        {
            _modelContext = modelContext;

        }

        public void Add(TextLanguage textLanguage)
        {
            _modelContext.TextLanguage.Add(textLanguage);
        }

        public async Task<IEnumerable<TextLanguage>> GetAllAsync()
        {
            return await _modelContext.TextLanguage.ToListAsync();
        }

        public async Task<TextLanguage> GetByIdAsync(int id)
        {
            return await _modelContext.TextLanguage.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<TextLanguage> GetByTextIdAsync(int textId)
        {
            return await _modelContext.TextLanguage.FirstOrDefaultAsync(p => p.TextId == textId);
        }

        public bool HasChanges()
        {
            return _modelContext.ChangeTracker.HasChanges();
        }

        public void Remove(TextLanguage model)
        {
            _modelContext.TextLanguage.Remove(model);
        }

        public async Task SaveAsync()
        {
            await _modelContext.SaveChangesAsync();
        }

        public void GetForeignKeys()
        {
            var key = _modelContext.Model.FindEntityType(typeof(TextLanguage)).GetForeignKeys();
        }

    }
}
