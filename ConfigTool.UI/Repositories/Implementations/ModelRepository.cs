using ConfigTool.DataAccess;
using ConfigTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly ModelContext _modelContext;

        public ModelRepository(ModelContext modelContext)
        {
            _modelContext = modelContext;

        }

        public IEnumerable<string> GetAllTables()
        {
          return  _modelContext.Model.GetEntityTypes()
                 .Select(t => t.GetTableName().Trim())
                 .Distinct()
                 .ToList();
        }
    }
}
