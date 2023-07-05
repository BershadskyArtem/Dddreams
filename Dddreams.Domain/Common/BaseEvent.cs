using MediatR;

namespace Dddreams.Domain.Common;

public class BaseEvent : INotification
{
    public DateTime DateOccured { get; set; } =DateTime.UtcNow;
}