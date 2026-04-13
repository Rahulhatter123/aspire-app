using Clean_Architecture_Sample.Application.Features.Product.Commands;
using Clean_Architecture_Sample.Domain.Interfaces;
using MediatR;

namespace Clean_Architecture_Sample.Application.Features.Product.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id, cancellationToken);
    }
}
