using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IModelRepository
    {
        IEnumerable<string> GetAllTables();
    }
}
