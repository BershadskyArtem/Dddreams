using Dddreams.Application.Common.Auth;
using Dddreams.Domain.Entities;
using Dddreams.Domain.Enums;

namespace Dddreams.Application.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<bool> AreFriends(Guid id, Guid otherId);
    public Task<DreamsRole?> GetRole(Guid id);
    Task<DreamsAccount?> GetByIdAsync(Guid id);
    Task<DreamsAccount?> GetByEmailAsync(string email);
    public Task<UserToken?> LoginUser(string email, string password);
    public Task<UserToken?> RegisterUser(string email, string password);
    public Task<UserToken?> RefreshToken(UserToken token);
    public Task<bool> Delete(Guid id);
}