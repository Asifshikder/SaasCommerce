using Ardalis.Specification;
using Project.Application.Products.Get.v1;
using Project.Core.Specifications;
using Project.Domain;
using Project.Core.Paging;
namespace Project.Application.Products.Search.v1;
public class SearchProductSpecs : EntitiesByPaginationFilterSpec<Product, ProductResponse>
{
    public SearchProductSpecs(SearchProductsCommand command)
        : base(command) =>
        Query
            .Include(p => p.Brand)
            .OrderBy(c => c.Name, !command.HasOrderBy())
            .Where(p => p.BrandId == command.BrandId!.Value, command.BrandId.HasValue)
            .Where(p => p.Price >= command.MinimumRate!.Value, command.MinimumRate.HasValue)
            .Where(p => p.Price <= command.MaximumRate!.Value, command.MaximumRate.HasValue);
}
