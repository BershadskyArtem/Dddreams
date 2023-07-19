using Dddreams.WebApi.Common;
using Dddreams.WebApi.Contracts.V1.Responses.Comments;

namespace Dddreams.WebApi.Contracts.V1.Responses.Dreams;

public class DreamResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorAvatar { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IllustationUrl { get; set; } = string.Empty;
    public int LikesCount { get; set; } = 0;
    public List<CommentResponse> FirstComments { get; set; } = new();
    public DateTime TimeOfDream { get; set; }
    public DreamVisibilityKind Visibility { get; set; }
}