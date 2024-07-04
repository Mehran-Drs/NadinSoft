using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NadinSoft.Application.Common;
using NadinSoft.Domain.Repositories;
using System.Net;

namespace NadinSoft.Application.CQRS.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        public DeleteProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Result<bool> baseResult;
            var product = await _repository.GetByIdAsync(request.ProductId);

            if (product != null)
            {
                if (product.CreatorId != request.CreatorId)
                {
                    baseResult = new Result<bool>(false, false, new List<Error>() { new Error("NOTALLOWED", "User Can Not Delete This Item") });
                    return baseResult;
                }

                product = _mapper.Map(request, product);
                var result = await _repository.DeleteAsync(product);
                if (result)
                {
                    baseResult = new Result<bool>(true);
                    return baseResult;
                }

                baseResult = new Result<bool>(false,false, new List<Error>() { new Error("SERVERERROR", "Something Went Wrong ...") });
                return baseResult;
            }

            baseResult = new Result<bool>(false, false, new List<Error>() { new Error("NOTFOUND", "Product Could Not Be Found") });
            return baseResult;
        }
    }
}
