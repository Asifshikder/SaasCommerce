using Project.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Project.Application.Products.EventHandlers;

public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) : INotificationHandler<ProductCreated>
{
    public async Task Handle(ProductCreated notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("handling product created domain event..");
        await Task.FromResult(notification);
        logger.LogInformation("finished handling product created domain event..");
    }
}

