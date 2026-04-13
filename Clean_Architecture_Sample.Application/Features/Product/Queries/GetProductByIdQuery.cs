using Clean_Architecture_Sample.Application.DTOs;
using MediatR;

namespace Clean_Architecture_Sample.Application.Features.Product.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductResponseDto?>;