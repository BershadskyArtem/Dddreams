using AutoMapper;
using Dddreams.Application.Features.Comments.Commands.Like;
using Dddreams.Application.Features.Comments.Commands.Unlike;
using Dddreams.Application.Features.Dreams.Commands.Delete;
using Dddreams.Application.Features.Dreams.Commands.LikeDream;
using Dddreams.Application.Features.Dreams.Commands.UnlikeDream;
using Dddreams.Infrastructure.Extensions;
using Dddreams.WebApi.Contracts.V1;
using Dddreams.WebApi.Contracts.V1.Requests.Comments;
using Dddreams.WebApi.Contracts.V1.Requests.Dreams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dddreams.WebApi.Controllers.V1;

[Authorize(Policy = "CanLikeOrDislike")]
public class LikesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public LikesController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }
    
    [HttpPost(ApiRoutes.Likes.GiveToDream)]
    
    public async Task<IActionResult> GiveToDream([FromBody] LikeDreamRequest likeDreamRequest)
    {
        var command = _mapper.Map<LikeDreamCommand>(likeDreamRequest);
        command.WhoRequested = HttpContext.GetUserId();
        var result = await _sender.Send(command);
        return result ? Ok() : BadRequest();
    }

    [HttpPost(ApiRoutes.Likes.GiveToComment)]
    public async Task<IActionResult> GiveToComment([FromBody] LikeCommentRequest likeCommentRequest)
    {
        var command = _mapper.Map<LikeCommentCommand>(likeCommentRequest);
        command.WhoRequested = HttpContext.GetUserId();
        var result = await _sender.Send(command);
        return result ? Ok() : BadRequest();
    }

    [HttpDelete(ApiRoutes.Likes.RemoveFromDream)]
    public async Task<IActionResult> RemoveFromDream([FromBody] UnlikeDreamRequest unlikeDreamRequest)
    {
        var command = _mapper.Map<UnlikeDreamCommand>(unlikeDreamRequest);
        command.WhoRequested = HttpContext.GetUserId();
        var result = await _sender.Send(command);
        return result ? Ok() : BadRequest();
    }
    
    [HttpDelete(ApiRoutes.Likes.RemoveFromComment)]
    public async Task<IActionResult> RemoveFromDream([FromBody] UnlikeCommentRequest unlikeCommentRequest)
    {
        var command = _mapper.Map<UnlikeCommentCommand>(unlikeCommentRequest);
        command.WhoRequested = HttpContext.GetUserId();
        var result = await _sender.Send(command);
        return result ? Ok() : BadRequest();
    }
}
