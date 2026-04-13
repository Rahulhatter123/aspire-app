using AutoMapper;
using Clean_Architecture_Sample.Application.DTOs;
using Clean_Architecture_Sample.Application.Features.Product.Queries;
using Clean_Architecture_Sample.Application.Helpers;
using Clean_Architecture_Sample.Domain.Interfaces;
using MediatR;

namespace Clean_Architecture_Sample.Application.Features.Product.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDto?>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductResponseDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return null;
        }

        var response = _mapper.Map<ProductResponseDto>(product);
        response.ImageFile = ImageFilePathHelper.ToClientPath(response.ImageFile);

        return response;
    }
}
