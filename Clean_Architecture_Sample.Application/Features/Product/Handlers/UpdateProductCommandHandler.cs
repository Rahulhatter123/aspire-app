using AutoMapper;
using Clean_Architecture_Sample.Application.Features.Product.Commands;
using Clean_Architecture_Sample.Domain.Interfaces;
using MediatR;

namespace Clean_Architecture_Sample.Application.Features.Product.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Clean_Architecture_Sample.Domain.Product>(request.Product);
        return await _repository.UpdateAsync(request.Id, product, cancellationToken);
    }
}
