using AutoMapper;
using MediatR;
using NadinSoft.Application.Common;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Result<int> baseResult;

            var product = _mapper.Map<Product>(request);

            var isExist = await _repository.AnyAsync(x=>x.ProduceDate == product.ProduceDate||x.ManufactureEmail == product.ManufactureEmail);
            if (isExist)
            {
                baseResult = new Result<int>(0,
                    false,
                    new List<Error>() 
                    { 
                        new Error("DUPLICATED","Product Produce Date Or Product Manufacture Email Is Can Not Be Duplicate" )
                    });

                return baseResult;
            }

            await _repository.CreateAsync(product);

            baseResult = new Result<int>(product.Id);

            return baseResult;
        }
    }
}
