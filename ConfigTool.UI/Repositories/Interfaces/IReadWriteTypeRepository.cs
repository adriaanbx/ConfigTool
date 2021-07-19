using ConfigTool.Models;
using ConfigTool.UI.Wrappers;
using System;
using System.Text;

namespace ConfigTool.UI.Repositories
{
    public interface IReadWriteTypeRepository : IGenericRepository<ReadWriteType, short, ReadWriteTypeWrapper>
    {

    }
}
