namespace Dddreams.WebApi.Contracts.V1.Responses.Auth;

public class AuthSuccededResponse
{
    public string RefreshToken { get; set; }
    public string Token { get; set; }
}