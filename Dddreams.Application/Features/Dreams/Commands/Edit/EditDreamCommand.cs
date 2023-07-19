using Dddreams.Application.Common.Queries;
using Dddreams.Domain.Entities;
using Dddreams.Domain.Enums;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.Edit;

public class EditDreamCommand : BaseAuditableQuery<bool>
{
    public Guid DreamId { get; set; }
    public string Title { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public string IllustrationUrl { get; set; } = string.Empty;
    public DateTime TimeOfDream { get; set; }
    public VisibilityKind Visibility { get; set; }
}