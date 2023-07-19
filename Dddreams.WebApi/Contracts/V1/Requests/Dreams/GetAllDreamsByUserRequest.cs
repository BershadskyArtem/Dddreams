namespace Dddreams.WebApi.Contracts.V1.Requests.Dreams;

public class GetAllDreamsByUserRequest
{
    public Guid DreamsAuthor { get; init; }
}