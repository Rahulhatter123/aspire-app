using AutoMapper;
using Clean_Architecture_Sample.Application.DTOs;
using Clean_Architecture_Sample.Application.Features.Product.Commands;
using Clean_Architecture_Sample.Domain.Interfaces;
using MediatR;

namespace Clean_Architecture_Sample.Application.Features.Product.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponseDto>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Clean_Architecture_Sample.Domain.Product>(request.Product);
        
        var created = await _repository.CreateAsync(product, cancellationToken);

        return _mapper.Map<ProductResponseDto>(created);
    }
}
