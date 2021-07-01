using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using ConfigTool.Models.Interfaces;

namespace ConfigTool.UI.Repositories
{
    public class GenericRepository<TEntity, TContext, TId> : IGenericRepository<TEntity, TId>
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
