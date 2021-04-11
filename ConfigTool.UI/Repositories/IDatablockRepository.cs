using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IDatablockRepository
    {
        Task<IEnumerable<DataBlock>> GetAllAsync();
        Task<DataBlock> GetByIdAsync(int id);
        Task SaveAsync();
        bool HasChanges();
        void Add(DataBlock plctag);
        void Remove(DataBlock model);
    }
}
