using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IPlctagRepository : IGenericRepository<Plctag, int>
    {
        Task<IEnumerable<TableItemPlctag>> GetPlctagLookupAsync();
    }
}
