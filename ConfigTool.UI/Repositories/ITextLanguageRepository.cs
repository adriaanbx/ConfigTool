using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface ITextLanguageRepository: IGenericRepository<TextLanguage, int>
    {
        Task<TextLanguage> GetByTextIdAsync(int textId);
    }
}
