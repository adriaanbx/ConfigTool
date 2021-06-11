using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ConfigTool.UI.Repositories
{
    public class UnitCategoryRepository : IUnitCategoryRepository
    {
        private readonly ModelContext _modelContext;

        public UnitCategoryRepository(ModelContext modelContext)
        {
            _modelContext = modelContext;

        }

        public void Add(UnitCategory unitCategory)
        {
           _modelContext.UnitCategory.Add(unitCategory);
        }

        public async Task<IEnumerable<UnitCategory>> GetAllAsync()
        {
            return await _modelContext.UnitCategory.ToListAsync();
        }

        public async Task<UnitCategory> GetByIdAsync(int id)
        {
            return await _modelContext.UnitCategory.FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool HasChanges()
        {
           return _modelContext.ChangeTracker.HasChanges();
        }

        public void Remove(UnitCategory model)
        {
            _modelContext.UnitCategory.Remove(model);
        }

        public async Task SaveAsync()
        {
            await _modelContext.SaveChangesAsync();
        }

        public void GetForeignKeys()
        {
            var key = _modelContext.Model.FindEntityType(typeof(UnitCategory)).GetForeignKeys();
        }
    }
}
