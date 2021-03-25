using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IPlctagRepository
    {
        Task<IEnumerable<Plctag>> GetAllAsync();
        Task<Plctag> GetByIdAsync(int id);
        Task SaveAsync();
        bool HasChanges();

    }
}
