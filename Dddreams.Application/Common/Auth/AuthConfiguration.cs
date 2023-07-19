namespace Dddreams.Application.Common.Auth;

public class AuthConfiguration
{
    public string SuperAdminEmail { get; set; }
    public string Key { get; set; }
    public int TokenLifeTimeInSeconds { get; set; }
    public int RefreshTokenLifeTimeInDays { get; set; }
}