using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ValueType = ConfigTool.Models.ValueType;

namespace ConfigTool.UI.Repositories
{
    public class ValueTypeRepository : IValueTypeRepository
    {
        private readonly ModelContext _modelContext;

        public ValueTypeRepository(ModelContext modelContext)
        {
            _modelContext = modelContext;

        }

        public void Add(ValueType valueType)
        {
           _modelContext.ValueType.Add(valueType);
        }

        public async Task<IEnumerable<ValueType>> GetAllAsync()
        {
            return await _modelContext.ValueType.ToListAsync();
        }

        public async Task<ValueType> GetByIdAsync(int id)
        {
            return await _modelContext.ValueType.FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool HasChanges()
        {
           return _modelContext.ChangeTracker.HasChanges();
        }

        public void Remove(ValueType model)
        {
            _modelContext.ValueType.Remove(model);
        }

        public async Task SaveAsync()
        {
            await _modelContext.SaveChangesAsync();
        }

        public void GetForeignKeys()
        {
            var key = _modelContext.Model.FindEntityType(typeof(ValueType)).GetForeignKeys();
        }
    }
}
