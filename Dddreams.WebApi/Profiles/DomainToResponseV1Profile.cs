using AutoMapper;
using Dddreams.Domain.Entities;
using Dddreams.Domain.Enums;
using Dddreams.WebApi.Common;
using Dddreams.WebApi.Contracts.V1.Responses.Comments;
using Dddreams.WebApi.Contracts.V1.Responses.Dreams;

namespace Dddreams.WebApi.Profiles;

public class DomainToResponseV1Profile : Profile
{
    public DomainToResponseV1Profile()
    {
        CreateMap<Dream, DreamResponse>()
            .ForMember(x => x.Visibility, opt => { opt.MapFrom(f => ToDtoVisibility(f.Visibility)); })
            .ForMember(x => x.AuthorId, opt => opt.MapFrom(f => f.Author.Id))
            .ForMember(x => x.FirstComments,
                opt =>
            {
                opt.MapFrom(
                    (f,_,_,ctx) =>
                        {
                            return f.Comments.Take(3).Select(com => ctx.Mapper.Map<CommentResponse>(com)).ToList();
                        }
                    );
            });
        
        
        CreateMap<Comment, CommentResponse>()
            .ForMember(x => x.LikesCount, opt =>
            {
                opt.MapFrom(f => f.Likes.Count());
            });

        CreateMap<List<Dream>, DreamsByUserResponse>()
            .ForMember(d => d.Dreams,
                (opt) =>
                {
                    opt.MapFrom((i, _, _, l) =>
                    {
                        return i.Select(dream => l.Mapper.Map<DreamResponse>(dream));
                    });
                });

    }

    private DreamVisibilityKind ToDtoVisibility(VisibilityKind kind)
    {
        switch (kind)
        {
            case VisibilityKind.Private:
                return DreamVisibilityKind.Private;
                break;
            case VisibilityKind.AllFriends:
                return DreamVisibilityKind.AllFriends;
                break;
            case VisibilityKind.Public:
                return DreamVisibilityKind.Public;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
        }
    }
}