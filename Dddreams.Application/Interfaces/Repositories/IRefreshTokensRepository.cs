using Dddreams.Application.Common.Auth;

namespace Dddreams.Application.Interfaces.Repositories;

public interface IRefreshTokensRepository
{
    public Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetByIdAsync(string id);
    void Update(RefreshToken token);
}