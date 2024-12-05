using ContactBookApi.Core.Abstractions;
using ContactBookApi.Data.Contexts;
using ContactBookApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ContactBookApi.Infrastructure.Repositories;

public class UnitOfWork(
    AppDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync()
    {
        UpdateAuditableEntities();
        return await dbContext.SaveChangesAsync();
    }

    private void UpdateAuditableEntities()
    {
        var entries = dbContext.ChangeTracker.Entries<IAuditable>();

        foreach (var entry in entries)
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(e => e.CreatedAt).CurrentValue = DateTimeOffset.UtcNow;
                    entry.Property(e => e.CreatedAt).CurrentValue = DateTimeOffset.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Property(e => e.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;
                    break;
            }
    }
}