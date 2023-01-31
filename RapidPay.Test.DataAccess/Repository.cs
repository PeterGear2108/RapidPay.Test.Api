using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace RapidPay.Test.DataAccess
{
    public class Repository<TEntity> where TEntity : class
    {
        protected readonly RapidPayDbContext _context;
        IDbContextTransaction transaction;
        IConfiguration _configuration;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
            _context = new RapidPayDbContext(_configuration);
        }
        public void BeginTransaction()
        {
            transaction = _context.Database.BeginTransaction();
        }
        public TEntity GetEntity(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAllEntities()
        {
            return _context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public void AddEntity(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddEntities(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void RemoveEntity(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveEntities(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void DeleteEntity(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Count(predicate);
        }

        public void SetEntityState(TEntity e, EntityState entityState)
        {
            _context.Entry(e).State = entityState;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void CommitTransaction()
        {
            transaction.Commit();
        }
        public void RollBackTransaction()
        {
            transaction.Rollback();
        }
    }
}
