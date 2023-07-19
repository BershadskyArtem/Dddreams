using Dddreams.Domain.Entities;

namespace Dddreams.Application.Interfaces.Repositories;

public interface IDreamsRepository
{
    public Task<List<Dream>?> GetAllByUserAsync(Guid userId);
    public Task<List<Dream>?> GetAllPublicByUserAsync(Guid userId);
    public Task<List<Dream>?> GetAllPublicByUserPaginationAsync(Guid userId, int pageSize, int pageNumber);
    public Task<List<Dream>?> GetAllByUserPaginationAsync(Guid userId, int pageSize, int pageNumber);
    public Task<List<Dream>?> GetAllPublicOrForFriendsByUserPaginationAsync(Guid userId, int pageSize, int pageNumber);
    public Task<List<Dream>?> GetAllPublicOrForFriendsByUserAsync(Guid userId);
    public Task<Dream?> GetByIdAsync(Guid id);
    public Task<bool> CreateAsync(Dream dream);
    public Task<bool> UpdateDreamAsync(Dream post);
    public Task<bool> DeleteDreamAsync(Guid id);
    void Edit(Dream dream);
}