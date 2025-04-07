using MediatR;

namespace Project.Core.Domain.Events;
public abstract record DomainEvent : IDomainEvent, INotification
{
    public DateTime RaisedOn { get; protected set; } = DateTime.UtcNow;
}
