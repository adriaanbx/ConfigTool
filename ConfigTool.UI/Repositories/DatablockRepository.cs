using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class DatablockRepository : IDatablockRepository
    {
        private readonly ModelContext _modelContext;

        public DatablockRepository(ModelContext modelContext)
        {
            _modelContext = modelContext;

        }

        public void Add(DataBlock datablock)
        {
           _modelContext.DataBlock.Add(datablock);
        }

        public async Task<IEnumerable<DataBlock>> GetAllAsync()
        {
            return await _modelContext.DataBlock.ToListAsync();
        }

        public async Task<DataBlock> GetByIdAsync(int id)
        {
            return await _modelContext.DataBlock.FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool HasChanges()
        {
           return _modelContext.ChangeTracker.HasChanges();
        }

        public void Remove(DataBlock model)
        {
            _modelContext.DataBlock.Remove(model);
        }

        public async Task SaveAsync()
        {
            await _modelContext.SaveChangesAsync();
        }

        public void GetForeignKeys()
        {
            var key = _modelContext.Model.FindEntityType(typeof(DataBlock)).GetForeignKeys();
        }
    }
}
