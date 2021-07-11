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
       
    }
}
