using Project.Core.Persistence;
using Project.Domain;
using Project.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Project.Application.Brands.Update.v1;
public sealed class UpdateBrandHandler(
    ILogger<UpdateBrandHandler> logger,
    [FromKeyedServices("catalog:brands")] IRepository<Brand> repository)
    : IRequestHandler<UpdateBrandCommand, UpdateBrandResponse>
{
    public async Task<UpdateBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var brand = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = brand ?? throw new BrandNotFoundException(request.Id);
        var updatedBrand = brand.Update(request.Name, request.Description);
        await repository.UpdateAsync(updatedBrand, cancellationToken);
        logger.LogInformation("Brand with id : {BrandId} updated.", brand.Id);
        return new UpdateBrandResponse(brand.Id);
    }
}
