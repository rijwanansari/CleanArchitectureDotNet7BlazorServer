using Application.Common.Interface;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        #region Properties
        private readonly ApplicationDBContext _context;

        protected DbSet<T> _entities;
        #endregion
        #region Ctor

        public EfRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        #endregion
        #region Utility
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }

            try
            {
                _context.SaveChanges();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return ex.ToString();
            }
        }
        #endregion

        #region Repository Methods
        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();

                return _entities;
            }
        }
        public  async Task<T> AddAsync(T entity)
        {
           await Entities.AddAsync(entity);
            return entity;
        }
        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public Task UpdateAsync(object Id, T entity)
        {
            T exist = _context.Set<T>().Find(Id);
            _context.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }
        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await Entities.FindAsync(id);
            if (entity == null)
                return false;

            Entities.Remove(entity);
            return true;
        }
        public async Task<bool> Delete(T entity)
        {
            Entities.Remove(entity);
            return true;
        }
        public async Task<bool> Delete(Expression<Func<T, bool>> where)
        {
            var entities = Entities.Where(where);
            Entities.RemoveRange(entities);
            return true;
        }
        public async Task<T> Get(object id)
        {
            return await Entities.FindAsync(id);
        }
        public async Task<T> Get(Expression<Func<T, bool>> where)
        {
            return await Entities.FirstOrDefaultAsync(where);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Entities;
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return Entities.Where(where);
        }
        public virtual void Save()
        {
            _context.SaveChanges();
        }
        public virtual void SaveAsync()
        {
            _context.SaveChangesAsync();
        }
        public virtual async Task<int> Count()
        {
            return await Entities.CountAsync();

        }
        public virtual async Task<int> Count(Expression<Func<T, bool>> where)
        {
            return await Entities.CountAsync(where);

        }
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        #endregion
    }
}
