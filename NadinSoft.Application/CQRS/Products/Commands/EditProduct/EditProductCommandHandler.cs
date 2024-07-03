using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Commands.EditProduct
{
    internal sealed class EditProductCommandHandler : IRequestHandler<EditProductCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly IValidator<EditProductCommand> _validator;
        public EditProductCommandHandler(IProductRepository repository, IMapper mapper, IValidator<EditProductCommand> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var product = await _repository.GetByIdAsync(request.Id);

            if (product != null)
            {
                if (product.CreatorId != request.CreatorId)
                {
                    throw new ValidationException(
                        new List<ValidationFailure>()
                        {
                              new ValidationFailure()
                                  {
                                      ErrorCode = "NoAccess",
                                      ErrorMessage = "User Can Not Edit Product"
                                  }
                        });
                }


                product = _mapper.Map(request, product);
                var result = await _repository.UpdateAsync(product);

                if (result)
                {
                    return result;
                }
                throw new ValidationException(
                    new List<ValidationFailure>()
                    {
                         new ValidationFailure()
                             {
                                 ErrorCode = "ServerError",
                                 ErrorMessage = "Something Went Wrong..."
                             }
                    });
            }

            throw new ValidationException(
                new List<ValidationFailure>()
                {
                     new ValidationFailure()
                          {
                             ErrorCode = "NotFound",
                             ErrorMessage = "Product Not Found"
                          }
                });  
        }
    }
}
