namespace Dddreams.WebApi.Contracts.V1.Requests.Comments;

public class EditCommentRequest
{
    public Guid CommentId { get; set; }
    public string NewContent { get; set; } = string.Empty;
}