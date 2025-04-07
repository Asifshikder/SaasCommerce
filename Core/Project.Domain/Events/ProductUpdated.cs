using Project.Core.Domain.Events;

namespace Project.Domain.Events;
public sealed record ProductUpdated : DomainEvent
{
    public Product? Product { get; set; }
}
