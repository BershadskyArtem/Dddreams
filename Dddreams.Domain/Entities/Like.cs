using Dddreams.Domain.Common;
using Dddreams.Domain.Enums;

namespace Dddreams.Domain.Entities;

public class Like : Likable
{
    public DreamsAccount Author { get; private set; }

    public static Like Create(DreamsAccount author, Guid likableId, LikableKind likableKind)
    {
        var result = new Like
        {
            Id = Guid.NewGuid(),
            Author = author,
            LikableId = likableId,
            Kind = likableKind
        };

        return result;
    }
    
    
    private Like()
    {
        
    }
    
    
}