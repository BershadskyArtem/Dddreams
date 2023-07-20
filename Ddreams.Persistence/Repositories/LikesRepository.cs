using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using Ddreams.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ddreams.Persistence.Repositories;

public class LikesRepository : ILikesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LikesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddAsync(Like like)
    {
        var alreadyExisted =
            await _dbContext.Likes.FirstOrDefaultAsync(
                l => l.AuthorId == like.AuthorId && l.LikableId == like.LikableId);

        if(alreadyExisted != null)
            return false;
        
        await _dbContext.Likes.AddAsync(like);
        return true;
    }

    public void Delete(Like like)
    {
        _dbContext.Likes.Remove(like);
    }

    public async Task<bool> DeleteByUserId(Guid whoLiked)
    {
        var likedEntity = await _dbContext.Likes.FirstOrDefaultAsync(d => d.AuthorId == whoLiked);
        if (likedEntity == null)
            return false;
        _dbContext.Likes.Remove(likedEntity);
        return true;
    }
}