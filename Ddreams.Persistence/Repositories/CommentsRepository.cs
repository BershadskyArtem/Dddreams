using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using Ddreams.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ddreams.Persistence.Repositories;

public class CommentsRepository : ICommentsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Comment>> GetAllFromPost(Guid postId)
    {
        var result = await _dbContext.Comments
            .Include(p => p.Likes)
            .Include(p => p.Parent)
            .Where(comment => comment.Parent.Id == postId)
            .ToListAsync();
        
        return result;
    }

    public Task<List<Comment>> GetAllFromPostPagination(Guid postId, int pageSize, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        var result = await _dbContext.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
        return result;
    }
}