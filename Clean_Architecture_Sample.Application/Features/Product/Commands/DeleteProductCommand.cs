using MediatR;

namespace Clean_Architecture_Sample.Application.Features.Product.Commands;

public record DeleteProductCommand(int Id) : IRequest<bool>;
