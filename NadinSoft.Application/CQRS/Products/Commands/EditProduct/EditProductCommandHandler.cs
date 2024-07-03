using AutoMapper;
using FluentValidation;
using MediatR;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Commands.EditProduct
{
    internal sealed class EditProductCommandHandler : IRequestHandler<EditProductCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly IValidator<EditProductCommand> _validator;
        internal EditProductCommandHandler(IProductRepository repository, IMapper mapper, IValidator<EditProductCommand> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var result = false;

            var product = await _repository.GetByIdAsync(request.Id);

            if (product != null)
            {
                product = _mapper.Map(request, product);
                result = await _repository.UpdateAsync(product);
            }

            return result;
        }
    }
}
