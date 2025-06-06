﻿using Project.Core.Persistence;
using Project.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Project.Application.Brands.Create.v1;
public sealed class CreateBrandHandler(
    ILogger<CreateBrandHandler> logger,
    [FromKeyedServices("catalog:brands")] IRepository<Brand> repository)
    : IRequestHandler<CreateBrandCommand, CreateBrandResponse>
{
    public async Task<CreateBrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var brand = Brand.Create(request.Name!, request.Description);
        await repository.AddAsync(brand, cancellationToken);
        logger.LogInformation("brand created {BrandId}", brand.Id);
        return new CreateBrandResponse(brand.Id);
    }
}
