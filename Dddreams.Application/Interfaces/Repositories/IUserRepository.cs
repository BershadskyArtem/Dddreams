using Dddreams.Domain.Enums;

namespace Dddreams.Application.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<bool> AreFriends(Guid id, Guid otherId);
    public Task<DreamsRole> GetRole(Guid id);
    Task<bool> AllowedToPost(Guid id);
    Task<bool> AllowedToComment(Guid userId);
}