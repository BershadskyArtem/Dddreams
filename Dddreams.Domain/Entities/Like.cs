using Dddreams.Domain.Common;
using Dddreams.Domain.Enums;

namespace Dddreams.Domain.Entities;

public class Like : Likable
{
    public Guid AuthorId { get; private set; }

    public static Like Create(Guid author, Guid likableId, LikableKind likableKind)
    {
        var result = new Like
        {
            Id = Guid.NewGuid(),
            AuthorId = author,
            LikableId = likableId,
            Kind = likableKind
        };

        return result;
    }
    
    
    private Like()
    {
        
    }
    
    
}