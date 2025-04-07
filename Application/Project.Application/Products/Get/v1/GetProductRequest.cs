using MediatR;

namespace Project.Application.Products.Get.v1;
public class GetProductRequest : IRequest<ProductResponse>
{
    public Guid Id { get; set; }
    public GetProductRequest(Guid id) => Id = id;
}
