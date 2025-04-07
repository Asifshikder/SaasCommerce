using Project.Core.Persistence;
using Project.Domain;
using Project.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Project.Application.Brands.Delete.v1;
public sealed class DeleteBrandHandler(
    ILogger<DeleteBrandHandler> logger,
    [FromKeyedServices("catalog:brands")] IRepository<Brand> repository)
    : IRequestHandler<DeleteBrandCommand>
{
    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var brand = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = brand ?? throw new BrandNotFoundException(request.Id);
        await repository.DeleteAsync(brand, cancellationToken);
        logger.LogInformation("Brand with id : {BrandId} deleted", brand.Id);
    }
}
