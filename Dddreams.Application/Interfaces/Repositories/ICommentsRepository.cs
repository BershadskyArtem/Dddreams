using Dddreams.Domain.Entities;

namespace Dddreams.Application.Interfaces.Repositories;

public interface ICommentsRepository
{
    public Task<List<Comment>> GetAllFromPost(Guid postId);
    public Task<List<Comment>> GetAllFromPostPagination(Guid postId, int pageSize, int pageNumber);
    public Task<bool> AddComment(Guid postId, Comment comment, Guid author);
    public Task<bool> EditComment(Guid commentId, Comment newData);
    public Task<bool> LikeComment(Guid commentId, Guid whoLiked);
    public Task<bool> UnlikeComment(Guid commentId, Guid whoRequested);
    public Task<bool> DeletComment(Guid commentId);
    Task<bool> OwnsComment(Guid commentId, Guid userId);
    Task<bool> SaveChangesAsync();
}