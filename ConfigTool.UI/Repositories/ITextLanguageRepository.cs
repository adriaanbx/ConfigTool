using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface ITextLanguageRepository
    {
        Task<IEnumerable<TextLanguage>> GetAllAsync();
        Task<TextLanguage> GetByIdAsync(int id);
        Task<TextLanguage> GetByTextIdAsync(int textId);
        Task SaveAsync();
        bool HasChanges();
        void Add(TextLanguage textLanguage);
        void Remove(TextLanguage model);
    }
}
