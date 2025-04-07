using Project.Core.Domain.Events;

namespace Project.Domain.Events;
public sealed record BrandCreated : DomainEvent
{
    public Brand? Brand { get; set; }
}
