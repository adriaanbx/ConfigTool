using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValueType = ConfigTool.Models.ValueType;

namespace ConfigTool.UI.Repositories
{
    public interface IUnitCategoryRepository
    {
        Task<IEnumerable<UnitCategory>> GetAllAsync();
        Task<UnitCategory> GetByIdAsync(int id);
        Task SaveAsync();
        bool HasChanges();
        void Add(UnitCategory unitCategory);
        void Remove(UnitCategory model);
    }
}
