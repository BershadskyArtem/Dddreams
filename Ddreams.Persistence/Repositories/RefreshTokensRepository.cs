using Dddreams.Application.Common.Auth;
using Dddreams.Application.Interfaces.Repositories;
using Ddreams.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ddreams.Persistence.Repositories;

public class RefreshTokensRepository : IRefreshTokensRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RefreshTokensRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(RefreshToken token)
    {
        await _dbContext.RefreshTokens.AddAsync(token);
    }

    public Task<RefreshToken?> GetByIdAsync(string id)
    {
        return _dbContext.RefreshTokens.SingleOrDefaultAsync(dream => dream.Token == id);
    }

    public void Update(RefreshToken token)
    {
        _dbContext.RefreshTokens.Update(token);
    }
}