using Ardalis.Specification;
using Project.Core.Paging;
using Project.Core.Specifications;
using Project.Application.Brands.Get.v1;
using Project.Domain;

namespace Project.Application.Brands.Search.v1;
public class SearchBrandSpecs : EntitiesByPaginationFilterSpec<Brand, BrandResponse>
{
    public SearchBrandSpecs(SearchBrandsCommand command)
        : base(command) =>
        Query
            .OrderBy(c => c.Name, !command.HasOrderBy())
            .Where(b => b.Name.Contains(command.Keyword), !string.IsNullOrEmpty(command.Keyword));
}
