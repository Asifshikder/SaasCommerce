using Project.Application.Brands.Get.v1;
using Project.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.Persistence;
using Project.Core.Paging;

namespace Project.Application.Brands.Search.v1;
public sealed class SearchBrandsHandler(
    [FromKeyedServices("catalog:brands")] IReadRepository<Brand> repository)
    : IRequestHandler<SearchBrandsCommand, PagedList<BrandResponse>>
{
    public async Task<PagedList<BrandResponse>> Handle(SearchBrandsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new SearchBrandSpecs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<BrandResponse>(items, request!.PageNumber, request!.PageSize, totalCount);
    }
}
