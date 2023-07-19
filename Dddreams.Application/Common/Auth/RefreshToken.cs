using Dddreams.Domain.Entities;

namespace Dddreams.Application.Common.Auth;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public string JwtId { get; set; }
    public DateTime CreationDate { get; set; }  
    public DateTime ExpiryDate { get; set; }
    public bool Invalidated { get; set; }
    public bool Used { get; set; }
    //public Guid UserId { get; set; }
    //public DreamsAccount User { get; set; }
}