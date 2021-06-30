using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IPlctagLookupDataService
    {
        Task<IEnumerable<TableItemPlctag>> GetPlctagLookupAsync();
        public IEnumerable<IForeignKey> GetForeignKeys();
        public Task SaveAsync();
        public bool HasChanges();
        void RejectChanges();

    }
}