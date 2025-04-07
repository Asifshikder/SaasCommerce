using MediatR;

namespace Project.Application.Products.Delete.v1;
public sealed record DeleteProductCommand(
    Guid Id) : IRequest;
