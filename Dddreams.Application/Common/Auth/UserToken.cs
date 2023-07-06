namespace Dddreams.Application.Common.Auth;

/// <summary>
/// Yes i undestrand that this is a leakage of abstraction.
/// If you have any ideas how to implement identity with clean architecture you are welcome.
/// </summary>
public class UserToken
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public bool IsSuccess { get; set; } = false;
    public List<string> Errors { get; set; } = new();
}