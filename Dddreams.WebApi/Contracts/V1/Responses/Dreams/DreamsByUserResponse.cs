namespace Dddreams.WebApi.Contracts.V1.Responses.Dreams;

public class DreamsByUserResponse
{
    public List<DreamResponse> Dreams { get; set; } = new();
}