using Dddreams.Domain.Enums;

namespace Dddreams.WebApi.Contracts.V1.Requests.Dreams;

public class CreateDreamRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? IllustrationUrl { get; set; } = string.Empty;
    public DateTime TimeOfDream { get; set; }
    public VisibilityKind Visibility { get; set; }
}