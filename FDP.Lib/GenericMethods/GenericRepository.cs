using FDP.Infrastructure;
using FDP.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace FDP.Lib;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly FdpContext _dbContext;
    private readonly DbSet<TEntity> _entitySet;

    public GenericRepository(FdpContext dbContext)
    {
        _dbContext = dbContext;
        _entitySet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> GetById(int id)
    {
        return await _entitySet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _entitySet.ToListAsync();
    }

    public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
    {
        return await _entitySet.Where(expression).ToListAsync();
    }

    public async Task<int> Create(TEntity entity)
    {
        await _entitySet.AddAsync(entity);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> Create(List<TEntity> entity)
    {
        await _entitySet.AddRangeAsync(entity);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> Update(TEntity entity)
    {
        //var dbEntity = _dbContext.Set<TEntity>().Find(entity.Id);
        //if (dbEntity != null)
        //{
        //    _dbContext.Entry(dbEntity).CurrentValues.SetValues(entity);
        //}
        _entitySet.Attach(entity);
        _entitySet.Entry(entity).State = EntityState.Modified;
        _entitySet.Update(entity);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> Update(List<TEntity> entity)
    {
        _entitySet.UpdateRange(entity);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> Delete(int id)
    {
        var entity = await GetById(id);
        _entitySet.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<int> Delete(List<TEntity> entities)
    {
        _entitySet.RemoveRange(entities);
        return await _dbContext.SaveChangesAsync();
    }
}