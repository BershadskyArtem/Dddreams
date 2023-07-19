namespace Dddreams.WebApi.Contracts.V1.Responses.Comments;

public class CommentResponse
{
    public Guid AuthorId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int LikesCount { get; set; } = 0;
}