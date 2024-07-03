using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
            var product = await _repository.GetByIdAsync(request.ProductId);

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
                                      ErrorMessage = "User Can Not Delete Product"
                                  }
                        });
                }

                product = _mapper.Map(request, product);
                var result = await _repository.DeleteAsync(product);
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
