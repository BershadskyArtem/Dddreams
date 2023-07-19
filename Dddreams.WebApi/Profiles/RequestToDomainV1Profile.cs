using AutoMapper;
using Dddreams.Application.Features.Comments.Commands.Create;
using Dddreams.Application.Features.Comments.Commands.Delete;
using Dddreams.Application.Features.Comments.Commands.Edit;
using Dddreams.Application.Features.Comments.Commands.Like;
using Dddreams.Application.Features.Comments.Commands.Unlike;
using Dddreams.Application.Features.Comments.Queries.GetAllFromPost;
using Dddreams.Application.Features.Dreams.Commands.Create;
using Dddreams.Application.Features.Dreams.Commands.Delete;
using Dddreams.Application.Features.Dreams.Commands.Edit;
using Dddreams.Application.Features.Dreams.Commands.LikeDream;
using Dddreams.Application.Features.Dreams.Commands.UnlikeDream;
using Dddreams.Application.Features.Dreams.Queries.GetAllDreamsByUser;
using Dddreams.Application.Features.User.Commands.Login;
using Dddreams.Application.Features.User.Commands.Refresh;
using Dddreams.Application.Features.User.Commands.Register;
using Dddreams.WebApi.Contracts.V1.Requests;
using Dddreams.WebApi.Contracts.V1.Requests.Comments;
using Dddreams.WebApi.Contracts.V1.Requests.Dreams;

namespace Dddreams.WebApi.Profiles;

public class RequestToDomainV1Profile : Profile
{
    public RequestToDomainV1Profile()
    {
        //User
        CreateMap<LoginAccountRequest, LoginUserCommand>();
        CreateMap<RefreshTokenRequest, RefreshTokenCommand>();
        CreateMap<RegisterAccountRequest, RegisterAccountCommand>();
        
        //Dreams
        CreateMap<CreateDreamRequest, CreateDreamCommand>();
        CreateMap<DeleteDreamRequest, DeleteDreamCommand>();
        CreateMap<EditDreamRequest, EditDreamCommand>();
        CreateMap<GetAllDreamsByUserRequest, GetAllDreamsByUserQuery>();
        CreateMap<LikeDreamRequest, LikeDreamCommand>();
        CreateMap<UnlikeDreamRequest, UnlikeDreamCommand>();
        
        //Comments
        CreateMap<CreateCommentRequest, CreateCommentCommand>();
        CreateMap<DeleteCommentRequest,DeleteCommentCommand>();
        CreateMap<EditCommentRequest, EditCommentCommand>();
        CreateMap<GetAllCommentsFromPostRequest, GetAllCommentsFromPostQuery>();
        CreateMap<LikeCommentRequest, LikeCommentCommand>();
        CreateMap<UnlikeCommentRequest, UnlikeCommentCommand>();
    }
}