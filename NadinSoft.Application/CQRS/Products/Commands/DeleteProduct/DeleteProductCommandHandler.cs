using AutoMapper;
using FluentValidation;
using MediatR;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Commands.DeleteProduct
{
    internal sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        public DeleteProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = false;

            var product = await _repository.GetByIdAsync(request.ProductId);

            if (product != null)
            {
                product = _mapper.Map(request, product);
                result = await _repository.UpdateAsync(product);
            }

            return result;
        }
    }
}
