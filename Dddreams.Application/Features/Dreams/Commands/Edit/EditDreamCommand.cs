using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.Edit;

public class EditDreamCommand : IRequest<bool>
{
    public Dream NewData { get; set; }
    public Guid WhoRequested { get; set; }
}