
using System.Linq.Expressions;

namespace DataAccess.Repositories.BaseRepository
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<IReadOnlyList<TEntity>> GetAll();
        Task<IReadOnlyList<TEntity>> GetAllByExpression(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetSingle(
             Expression<Func<TEntity, bool>> filter = null,
             params Expression<Func<TEntity, object>>[] includes);

        Task Update(TEntity entity);
        void DeleteById(Guid id);

        bool Exist(Guid id);
        Task SaveChanges();
    }
}
