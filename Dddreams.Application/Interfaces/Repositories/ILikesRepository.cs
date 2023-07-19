using Dddreams.Domain.Entities;

namespace Dddreams.Application.Interfaces.Repositories;

public interface ILikesRepository
{
    Task AddAsync(Like like);
    void Delete(Like like);
}