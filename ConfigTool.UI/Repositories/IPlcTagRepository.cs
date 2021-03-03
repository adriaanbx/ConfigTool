using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IPlcTagRepository
    {
        Task<IEnumerable<Plctag>> GetAll();
    }
}
