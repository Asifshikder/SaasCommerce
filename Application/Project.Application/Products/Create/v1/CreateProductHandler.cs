using Project.Core.Persistence;
using Project.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Project.Application.Products.Create.v1;
public sealed class CreateProductHandler(
    ILogger<CreateProductHandler> logger,
    [FromKeyedServices("catalog:products")] IRepository<Product> repository)
    : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var product = Product.Create(request.Name!, request.Description, request.Price, request.BrandId);
        await repository.AddAsync(product, cancellationToken);
        logger.LogInformation("product created {ProductId}", product.Id);
        return new CreateProductResponse(product.Id);
    }
}
