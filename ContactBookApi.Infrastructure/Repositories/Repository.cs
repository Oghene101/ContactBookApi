using ContactBookApi.Core.Abstractions;
using ContactBookApi.Data.Contexts;
using ContactBookApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactBookApi.Infrastructure.Repositories;

public class Repository<TEntity>(AppDbContext dbContext) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public IQueryable<TEntity> GetAll()
        => _dbSet;

    public async Task<TEntity?> FindByIdAsync(string id)
        => await _dbSet.FindAsync(id);

    public void Update(TEntity entity)
        => _dbSet.Update(entity);
    
    public void Delete(TEntity entity)
        => _dbSet.Remove(entity);
}