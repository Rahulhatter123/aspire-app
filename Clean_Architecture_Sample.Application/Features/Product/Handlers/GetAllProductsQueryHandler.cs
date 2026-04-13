using AutoMapper;
using Clean_Architecture_Sample.Application.DTOs;
using Clean_Architecture_Sample.Application.Features.Product.Queries;
using Clean_Architecture_Sample.Application.Helpers;
using Clean_Architecture_Sample.Domain.Interfaces;
using MediatR;

namespace Clean_Architecture_Sample.Application.Features.Product.Handlers;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductResponseDto>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ProductResponseDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);
        var response = _mapper.Map<IReadOnlyList<ProductResponseDto>>(products);

        foreach (var product in response)
        {
            product.ImageFile = ImageFilePathHelper.ToClientPath(product.ImageFile);
        }

        return response;
    }
}
