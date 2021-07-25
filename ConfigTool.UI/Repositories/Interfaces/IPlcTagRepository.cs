using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
using ConfigTool.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IPlctagRepository : IGenericRepository<Plctag, int, PlctagWrapper>
    {
        public Task<IEnumerable<LookupItem<int>>> GetAllPressParametersLookupAsync();
        public Task<IEnumerable<LookupItem<int>>> GetAllParametersLookupAsync();
        public Task<IEnumerable<LookupItem<int>>> GetAllPlcMappingLookupAsync();
    }
}
