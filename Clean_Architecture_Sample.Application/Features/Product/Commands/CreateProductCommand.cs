using Clean_Architecture_Sample.Application.DTOs;
using MediatR;

namespace Clean_Architecture_Sample.Application.Features.Product.Commands;

public record CreateProductCommand(ProductDto Product) : IRequest<ProductResponseDto>;
