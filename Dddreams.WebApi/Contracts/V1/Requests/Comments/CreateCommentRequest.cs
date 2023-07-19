namespace Dddreams.WebApi.Contracts.V1.Requests.Comments;

public class CreateCommentRequest
{
    public string Content { get; set; } 
    public Guid DreamId { get; set; }
}