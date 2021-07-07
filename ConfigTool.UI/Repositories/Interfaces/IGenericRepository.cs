using ConfigTool.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public interface IGenericRepository<TEntity, TId>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<LookupItem<TId>>> GetAllLookupAsync();
        Task<TEntity> GetByIdAsync(TId id);
        Task SaveAsync();
        bool HasChanges();
        void Add(TEntity model);
        void Remove(TEntity model);
        IEnumerable<IForeignKey> GetForeignKeys();
        TId GetMaxId();
        void RejectChanges();
    }
}
