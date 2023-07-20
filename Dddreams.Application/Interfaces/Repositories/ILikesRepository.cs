using Dddreams.Domain.Entities;

namespace Dddreams.Application.Interfaces.Repositories;

public interface ILikesRepository
{
    Task<bool> AddAsync(Like like);
    void Delete(Like like);
    Task<bool> DeleteByUserId(Guid whoLiked);
}