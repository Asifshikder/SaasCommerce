using Project.Core.Domain.Events;

namespace Project.Domain.Events;
public sealed record BrandUpdated : DomainEvent
{
    public Brand? Brand { get; set; }
}
