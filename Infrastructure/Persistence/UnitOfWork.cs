using Application.Common.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;

namespace Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private readonly ApplicationDBContext _context;
        IDbContextTransaction dbContextTransaction;
        private Hashtable _repositories;
        #endregion
        #region Ctor
        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }
        #endregion
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            _repositories ??= new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(EfRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void BeginTransaction()
        {
            dbContextTransaction = _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            dbContextTransaction?.Commit();
        }
        public void RollbackTransaction()
        {
            dbContextTransaction?.Rollback();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
