using Project.Core.Domain.Events;

namespace Project.Domain.Events;
public sealed record ProductCreated : DomainEvent
{
    public Product? Product { get; set; }
}
