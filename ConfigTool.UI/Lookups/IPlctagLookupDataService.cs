using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IPlctagLookupDataService
    {
        Task<IEnumerable<NavigationItemPlctag>> GetPlctagLookupAsync();
        public IEnumerable<IForeignKey> GetForeignKeys();
    }
}