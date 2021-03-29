using ConfigTool.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IPlctagLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetPlctagLookupAsync();
    }
}