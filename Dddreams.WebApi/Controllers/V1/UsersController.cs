using AutoMapper;
using Dddreams.Application.Features.User.Commands.Login;
using Dddreams.Application.Features.User.Commands.Refresh;
using Dddreams.Application.Features.User.Commands.Register;
using Dddreams.Infrastructure.Auth;
using Dddreams.WebApi.Contracts.V1;
using Dddreams.WebApi.Contracts.V1.Requests;
using Dddreams.WebApi.Contracts.V1.Responses.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dddreams.WebApi.Controllers.V1;

public class UsersController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    
    public UsersController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost(ApiRoutes.Users.Register)]
    public async Task<IActionResult> Register([FromBody]RegisterAccountRequest request)
    {
        var command = _mapper.Map<RegisterAccountCommand>(request);
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(new AuthFailedResponse()
            {
                Errors = result.Errors
            });
        return Ok(new AuthSuccededResponse()
        {
            RefreshToken = result.RefreshToken,
            Token = result.Token
        });
    }
    
    [HttpPost(ApiRoutes.Users.Login)]
    public async Task<IActionResult> Login([FromBody]LoginAccountRequest request)
    {
        var command = _mapper.Map<LoginUserCommand>(request);
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(new AuthFailedResponse()
            {
                Errors = result.Errors
            });
        return Ok(new AuthSuccededResponse()
        {
            RefreshToken = result.RefreshToken,
            Token = result.Token
        });
    }
    
    
    [HttpPost(ApiRoutes.Users.Refresh)]
    public async Task<IActionResult> Refresh([FromBody]RefreshTokenRequest request)
    {
        var command = _mapper.Map<RefreshTokenCommand>(request);
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(new AuthFailedResponse()
            {
                Errors = result.Errors
            });
        return Ok(new AuthSuccededResponse()
        {
            RefreshToken = result.RefreshToken,
            Token = result.Token
        });
    }
}