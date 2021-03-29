using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class PlctagRepository : IPlctagRepository
    {
        private readonly ModelContext _modelContext;

        public PlctagRepository(ModelContext modelContext)
        {
            _modelContext = modelContext;

        }

        public void Add(Plctag plctag)
        {
           _modelContext.Plctag.Add(plctag);
        }

        public async Task<IEnumerable<Plctag>> GetAllAsync()
        {
            return await _modelContext.Plctag.ToListAsync();
        }

        public async Task<Plctag> GetByIdAsync(int id)
        {
            return await _modelContext.Plctag.FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool HasChanges()
        {
           return _modelContext.ChangeTracker.HasChanges();
        }

        public void Remove(Plctag model)
        {
            _modelContext.Plctag.Remove(model);
        }

        public async Task SaveAsync()
        {
            await _modelContext.SaveChangesAsync();
        }
    }
}
