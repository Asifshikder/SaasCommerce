using MediatR;

namespace Project.Application.Brands.Delete.v1;
public sealed record DeleteBrandCommand(
    Guid Id) : IRequest;
