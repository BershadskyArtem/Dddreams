using Dddreams.Domain.Common;

namespace Dddreams.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : BaseEntity;

    Task<int> Save(CancellationToken cancellationToken);

    Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

    Task Rollback();
}