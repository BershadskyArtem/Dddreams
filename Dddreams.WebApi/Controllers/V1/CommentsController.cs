using AutoMapper;
using Dddreams.Application.Features.Comments.Commands.Create;
using Dddreams.Application.Features.Comments.Commands.Delete;
using Dddreams.Application.Features.Comments.Commands.Edit;
using Dddreams.Application.Features.Comments.Queries.GetAllFromPost;
using Dddreams.Infrastructure.Extensions;
using Dddreams.WebApi.Contracts.V1;
using Dddreams.WebApi.Contracts.V1.Requests.Comments;
using Dddreams.WebApi.Contracts.V1.Responses.Comments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dddreams.WebApi.Controllers.V1;

public class CommentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    
    public CommentsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet(ApiRoutes.Comments.GetFromDream)]
    public async Task<IActionResult> GetFromDream([FromRoute]Guid dreamId)
    {
        var query = new GetAllCommentsFromPostQuery()
        {
            PostId = dreamId,
            WhoRequested = HttpContext.GetUserId()
        };

        var result = await _sender.Send(query);
        var resultDto = _mapper.Map<CommentsFromDreamResponse>(result);
        return Ok(resultDto);
    }


    [HttpPost(ApiRoutes.Comments.Create)]
    public async Task<IActionResult> Create([FromBody]CreateCommentRequest createCommentRequest)
    {
        var command = _mapper.Map<CreateCommentCommand>(createCommentRequest);
        command.WhoRequested = HttpContext.GetUserId();
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut(ApiRoutes.Comments.Update)]
    public async Task<IActionResult> Update([FromBody]EditCommentRequest editCommentRequest)
    {
        var command = _mapper.Map<EditCommentCommand>(editCommentRequest);
        command.WhoRequested = HttpContext.GetUserId();
        var result = await _sender.Send(command);
        return Ok(result);
    }


    [HttpDelete(ApiRoutes.Comments.Delete)]
    public async Task<IActionResult> Delete([FromRoute]Guid commentId)
    {
        var command = new DeleteCommentCommand()
        {
            CommentId = commentId,
            WhoRequested = HttpContext.GetUserId()
        };
        var result = await _sender.Send(command);
        return NoContent();
    }
    
    
}