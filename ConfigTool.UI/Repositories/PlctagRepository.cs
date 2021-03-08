using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class PlctagRepository : IPlcTagRepository
    {
        private readonly ModelContext _modelContext;

        public PlctagRepository(ModelContext modelContext)
        {
            _modelContext = modelContext;

        }

        public async Task<IEnumerable<Plctag>> GetAllAsync()
        {
            return await _modelContext.Plctag.ToListAsync();
        }
    }
}
