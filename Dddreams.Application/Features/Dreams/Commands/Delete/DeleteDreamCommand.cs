using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.Delete;

public class DeleteDreamCommand : IRequest<bool>
{
    public Guid WhoRequested { get; set; }
    public Guid DreamId { get; set; }
}