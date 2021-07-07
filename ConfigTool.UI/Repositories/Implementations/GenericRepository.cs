using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using ConfigTool.Models.Interfaces;
using ConfigTool.Models;

namespace ConfigTool.UI.Repositories
{
    public abstract class GenericRepository<TEntity, TContext, TId> : IGenericRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TContext : DbContext
    {
        protected readonly TContext _context;

        protected GenericRepository(TContext context)
        {
            _context = context;
        }

        public void Add(TEntity model)
        {
            _context.Set<TEntity>().Add(model);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public abstract Task<IEnumerable<LookupItem<TId>>> GetAllLookupAsync();


        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public IEnumerable<IForeignKey> GetForeignKeys()
        {
            return _context.Model.FindEntityType(typeof(TEntity)).GetForeignKeys();
        }

        public TId GetMaxId()
        {
            return _context.Set<TEntity>().Max(m => m.Id);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void RejectChanges()
        {
            var changedEntries = _context.ChangeTracker.Entries()
                 .Where(e => e.State != EntityState.Unchanged);

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;

                    // Where a Key Entry has been deleted, reloading from the source is required to ensure that the entity's relationships are restored (undeleted).
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        public void Remove(TEntity model)
        {
            _context.Set<TEntity>().Remove(model);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
