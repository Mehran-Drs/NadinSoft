using AutoMapper;
using FluentValidation;
using MediatR;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Commands.CreateProduct
{
    internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly IValidator<CreateProductCommand> _validator;

        internal CreateProductCommandHandler(IMapper mapper, IProductRepository repository, IValidator<CreateProductCommand> validator)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var product = _mapper.Map<Product>(request);

            await _repository.CreateAsync(product);

            return product.Id;
        }
    }
}
