
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories.BaseRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly BookContext _bookContext;
        protected DbSet<TEntity> _dbSet;

        public Repository(BookContext bookContext)
        {
            _bookContext = bookContext;
            _dbSet = _bookContext.Set<TEntity>();
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _bookContext.SaveChangesAsync();

            return entity;
        }

        public void DeleteById(Guid id)
        {
            var entity = _dbSet.FirstOrDefault(a =>a.Id == id);

            if(entity != null)
            {
                _bookContext.Remove(entity);
                _bookContext.SaveChanges();
            }
        }

        public bool Exist(Guid id)
        {
            return  _dbSet.Any(a => a.Id == id);
        }

        public async Task<IReadOnlyList<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllByExpression(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet
            .Where(filter)
            .ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<TEntity> GetSingle(
            Expression<Func<TEntity, bool>> filter = null, 
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(filter);
                }
            }

            return query.FirstOrDefaultAsync();
        }

        public async Task SaveChanges()
        {
            await _bookContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await _bookContext.SaveChangesAsync();
        }
    }
}
