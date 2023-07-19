using AutoMapper;
using Dddreams.Application.Features.Dreams.Commands.Create;
using Dddreams.Application.Features.Dreams.Commands.Delete;
using Dddreams.Application.Features.Dreams.Commands.Edit;
using Dddreams.Application.Features.Dreams.Queries.GetAllDreamsByUser;
using Dddreams.Infrastructure.Auth;
using Dddreams.Infrastructure.Extensions;
using Dddreams.WebApi.Contracts.V1;
using Dddreams.WebApi.Contracts.V1.Requests.Dreams;
using Dddreams.WebApi.Contracts.V1.Responses.Dreams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dddreams.WebApi.Controllers.V1;

public class DreamsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public DreamsController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpGet(ApiRoutes.Dreams.GetAllByUser)]
    public async Task<IActionResult> GetAllByUser([FromRoute] Guid userId)
    {
        var request = new GetAllDreamsByUserQuery()
        {
            DreamsAuthor = userId,
            WhoRequested = HttpContext.GetUserId()
        };
        var result = await _sender.Send(request);
        var resultDto = _mapper.Map<DreamsByUserResponse>(result);
        return Ok(resultDto);
    }

    [HttpPost(ApiRoutes.Dreams.Create)]
    [Authorize(Policy = "CanPostOrDelete")]
    public async Task<IActionResult> Create([FromBody] CreateDreamRequest createRequest)
    {
        var command = _mapper.Map<CreateDreamCommand>(createRequest);
        command.WhoRequested = HttpContext.GetUserId();
        var result = await _sender.Send(command);
        var resultDto = _mapper.Map<DreamResponse>(result);
        return Ok(resultDto);
    }
    
    [Authorize(Policy = "CanPostOrDelete")]
    [HttpPut(ApiRoutes.Dreams.Update)]
    public async Task<IActionResult> Update([FromBody] EditDreamRequest createRequest)
    {
        var command = _mapper.Map<EditDreamCommand>(createRequest);
        command.WhoRequested = HttpContext.GetUserId();
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [Authorize(Policy = "CanPostOrDelete")]
    [HttpDelete(ApiRoutes.Dreams.Delete)]
    public async Task<IActionResult> Delete([FromBody] DeleteDreamRequest deleteRequest)
    {
        var command = _mapper.Map<DeleteDreamCommand>(deleteRequest);
        command.WhoRequested = HttpContext.GetUserId();
        var result = await _sender.Send(command);
        return result ? NoContent() : BadRequest();
    }
}