using Dddreams.WebApi.Common;

namespace Dddreams.WebApi.Contracts.V1.Requests.Dreams;

public class EditDreamRequest
{
    public Guid DreamId { get; set; }
    public string Title { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public string IllustrationUrl { get; set; } = string.Empty;
    public DateTime TimeOfDream { get; set; }
    public DreamVisibilityKind Visibility { get; set; }
}