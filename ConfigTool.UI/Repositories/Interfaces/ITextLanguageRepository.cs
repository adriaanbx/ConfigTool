using ConfigTool.Models;
using ConfigTool.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface ITextLanguageRepository: IGenericRepository<TextLanguage, int, TextLanguageWrapper>
    {
        Task<TextLanguage> GetByTextIdAsync(int textId);
    }
}
