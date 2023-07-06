using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using Dddreams.Domain.Enums;
using Ddreams.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ddreams.Persistence.Repositories;

public class DreamsRepository : IDreamsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DreamsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Dream>?> GetAllByUserAsync(Guid userId)
    {
        return await _dbContext.Dreams  
            .Include(dream => dream.Author)
            .Include(dream => dream.Comments)
            .Include(dream => dream.Likes)
            .Where(dream => dream.Author.Id == userId).ToListAsync();
    }

    public async Task<List<Dream>?> GetAllPublicByUserAsync(Guid userId)
    {
        return await _dbContext.Dreams
            .Include(dream => dream.Author)
            .Include(dream => dream.Comments)
            .Include(dream => dream.Likes)
            .Where(dream => dream.Author.Id == userId)
            .Where(dream => dream.Visibility == VisibilityKind.Public)
            .ToListAsync();
    }

    public Task<List<Dream>?> GetAllPublicByUserPaginationAsync(Guid userId, int pageSize, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public Task<List<Dream>?> GetAllByUserPaginationAsync(Guid userId, int pageSize, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public Task<List<Dream>?> GetAllPublicOrForFriendsByUserPaginationAsync(Guid userId, int pageSize, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Dream>?> GetAllPublicOrForFriendsByUserAsync(Guid userId)
    {
        return await _dbContext.Dreams
            .Include(dream => dream.Author)
            .Include(dream => dream.Comments)
            .Include(dream => dream.Likes)
            .Where(dream => dream.Author.Id == userId)
            .Where(dream => dream.Visibility == VisibilityKind.Public || dream.Visibility == VisibilityKind.AllFriends)
            .ToListAsync();
    }

    public async Task<Dream?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Dreams
            .Include(dream => dream.Author)
            .Include(dream => dream.Comments)
            .Include(dream => dream.Likes)
            .FirstOrDefaultAsync(dream => dream.Author.Id == id);
    }

    public async Task<bool> CreateAsync(Dream dream)
    {
        await _dbContext.Dreams.AddAsync(dream);
        return true;
    }
    
    public Task<bool> UpdateDreamAsync(Dream post)
    {
        _dbContext.Dreams.Update(post);
        return Task.FromResult(true);
    }

    public async Task<bool> DeleteDreamAsync(Guid id)
    {
        var d = await _dbContext.Dreams.FirstOrDefaultAsync(d => d.Id == id);
        if (d == null)
            return false;

        _dbContext.Dreams.Remove(d);
        return true;
    }
}