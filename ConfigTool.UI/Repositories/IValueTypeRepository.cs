using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValueType = ConfigTool.Models.ValueType;

namespace ConfigTool.UI.Repositories
{
    public interface IValueTypeRepository
    {
        Task<IEnumerable<ValueType>> GetAllAsync();
        Task<ValueType> GetByIdAsync(int id);
        Task SaveAsync();
        bool HasChanges();
        void Add(ValueType plctag);
        void Remove(ValueType model);
    }
}
