using Project.Application.Products.Get.v1;
using Project.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.Persistence;
using Project.Core.Paging;


namespace Project.Application.Products.Search.v1;
public sealed class SearchProductsHandler(
    [FromKeyedServices("catalog:products")] IReadRepository<Product> repository)
    : IRequestHandler<SearchProductsCommand, PagedList<ProductResponse>>
{
    public async Task<PagedList<ProductResponse>> Handle(SearchProductsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new SearchProductSpecs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<ProductResponse>(items, request!.PageNumber, request!.PageSize, totalCount);
    }
}

