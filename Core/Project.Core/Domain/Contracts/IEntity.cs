using System.Collections.ObjectModel;
using Project.Core.Domain.Events;

namespace Project.Core.Domain.Contracts;

public interface IEntity
{
    Collection<DomainEvent> DomainEvents { get; }
}

public interface IEntity<out TId> : IEntity
{
    TId Id { get; }
}
