using Dddreams.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ddreams.Persistence.Interceptors;

public class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var dbContext = eventData.Context;
        
        if(dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var entriesToAudit = dbContext
            .ChangeTracker
            .Entries<AuditableEntity>();
        
        foreach (var entityEntry in entriesToAudit)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(p => p.CreatedDate).CurrentValue = DateTime.UtcNow;
            }
            
            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(p => p.ModifiedDate).CurrentValue = DateTime.UtcNow;
            }
            
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}