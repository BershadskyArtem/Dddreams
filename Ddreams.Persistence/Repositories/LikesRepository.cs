using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using Ddreams.Persistence.Contexts;

namespace Ddreams.Persistence.Repositories;

public class LikesRepository : ILikesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LikesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Like like)
    {
        await _dbContext.Likes.AddAsync(like);
    }

    public void Delete(Like like)
    {
        _dbContext.Likes.Remove(like);
    }
}