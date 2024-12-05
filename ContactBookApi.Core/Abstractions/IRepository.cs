using ContactBookApi.Domain.Entities;

namespace ContactBookApi.Core.Abstractions;

public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    public Task AddAsync(TEntity entity);
    public IQueryable<TEntity> GetAll();
    public Task<TEntity?> FindByIdAsync(string id);
    public void Update(TEntity entity);
    public void Delete(TEntity entity);
}