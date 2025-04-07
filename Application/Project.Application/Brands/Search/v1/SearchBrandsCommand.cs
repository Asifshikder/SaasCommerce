using Project.Application.Brands.Get.v1;
using MediatR;
using Project.Core.Paging;

namespace Project.Application.Brands.Search.v1;

public class SearchBrandsCommand : PaginationFilter, IRequest<PagedList<BrandResponse>>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
