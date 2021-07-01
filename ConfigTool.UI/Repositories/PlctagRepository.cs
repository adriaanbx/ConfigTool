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
    public class PlctagRepository : GenericRepository<Plctag, ModelContext, int>, IPlctagRepository
    {
        public PlctagRepository(ModelContext modelContext) : base(modelContext)
        {
        }
    }
}
