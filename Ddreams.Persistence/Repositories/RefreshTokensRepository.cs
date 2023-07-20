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
        var i = Guid.Parse(id);
        return _dbContext.RefreshTokens.SingleOrDefaultAsync(dream => dream.Id == i);
    }

    public void Update(RefreshToken token)
    {
        _dbContext.RefreshTokens.Update(token);
    }
}