using Dddreams.Domain.Entities;

namespace Dddreams.Application.Interfaces.Repositories;

public interface ICommentsRepository
{
    public Task<List<Comment>> GetAllFromPost(Guid postId);
    public Task<List<Comment>> GetAllFromPostPagination(Guid postId, int pageSize, int pageNumber);
    Task<Comment?> GetByIdAsync(Guid id);
    void Update(Comment comment);
}