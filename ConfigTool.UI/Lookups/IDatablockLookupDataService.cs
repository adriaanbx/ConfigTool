using ConfigTool.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Lookups
{
    public interface IDatablockLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetDatablockLookupAsync();
    }
}