using Dddreams.Application.Common.Auth;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using Dddreams.Domain.Enums;
using Ddreams.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ddreams.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AreFriends(Guid id, Guid otherId)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .Include(user => user.Friends)
            .FirstOrDefaultAsync(user => user.Id == otherId);

        var friend = user?.Friends.FirstOrDefault(friend => friend.Id == otherId);

        return friend != null;
    }

    public async Task<DreamsRole?> GetRole(Guid id)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id);
        
        return user?.Role;
    }

    public async Task<DreamsAccount?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public Task<DreamsAccount?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<UserToken?> LoginUser(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<UserToken?> RegisterUser(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<UserToken?> RefreshToken(UserToken token)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}