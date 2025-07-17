using System.Linq.Expressions;

namespace FDP.Lib;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetById(int id);
    Task<List<TEntity>> GetAll();
    Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression);
    Task<int> Update(TEntity entity);
    Task<int> Update(List<TEntity> entity);
    Task<int> Create(TEntity entity);
    Task<int> Create(List<TEntity> entity);
    Task<TEntity> Delete(int id);
    Task<int> Delete(List<TEntity> entities);
}
