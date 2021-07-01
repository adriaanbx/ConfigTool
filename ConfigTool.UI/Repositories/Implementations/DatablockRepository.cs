using ConfigTool.DataAccess;
using ConfigTool.Models;
using System;
using System.Text;

namespace ConfigTool.UI.Repositories
{
    public class DatablockRepository : GenericRepository<DataBlock, ModelContext, int>, IDatablockRepository
    {
        public DatablockRepository(ModelContext modelContext) : base(modelContext)
        {
        }
    }
}
