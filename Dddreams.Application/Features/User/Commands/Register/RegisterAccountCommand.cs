using Dddreams.Application.Common.Auth;
using Dddreams.Application.Common.Queries;

namespace Dddreams.Application.Features.User.Commands.Register;

public class RegisterAccountCommand : BaseAuditableQuery<UserToken>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}