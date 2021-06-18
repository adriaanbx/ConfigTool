using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Lookups
{
    public interface ITextLanguageLookupDataService
    {
        Task<IEnumerable<LookupItem<int>>> GetTextLanguageLookupAsync();
    }
}