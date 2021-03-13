using ConfigTool.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IPlctagLookupDataRepository
    {
        Task<IEnumerable<LookupItem>> GetPlctagLookupAsync();
    }
}