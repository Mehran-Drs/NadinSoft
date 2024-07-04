using AutoMapper;
using MediatR;
using NadinSoft.Application.Common;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Commands.EditProduct
{
    public sealed class EditProductCommandHandler : IRequestHandler<EditProductCommand, Result<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        public EditProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<bool>> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            Result<bool> baseResult;
            var product = await _repository.GetByIdAsync(request.Id);

            if (product != null)
            {
                if (product.CreatorId != request.CreatorId)
                {
                    baseResult = new Result<bool>(false, false, new List<Error>() { new Error("NOTALLOWED", "User Cannot Edit This Item") });
                    return baseResult;
                }

                var isExist = await _repository.AnyAsync(x => x.ProduceDate == product.ProduceDate || x.ManufactureEmail == product.ManufactureEmail && x.Id != product.Id);
                if (isExist)
                {
                    baseResult = new Result<bool>(false,
                        false,
                        new List<Error>()
                        {
                        new Error("DUPLICATED","Product Produce Date Or Product Manufacture Email Can Not Be Duplicate" )
                        });

                    return baseResult;
                }


                product = _mapper.Map(request, product);
                var result = await _repository.UpdateAsync(product);

                if (result)
                {
                    baseResult = new Result<bool>(true);
                    return baseResult;
                }
                baseResult = new Result<bool>(false, false, new List<Error>() { new Error("SERVERERROR", "Something Went Wrong ...") });
                return baseResult;
            }

            baseResult = new Result<bool>(false, false, new List<Error>() { new Error("NOTFOUND", "Product Could Not Be Found") });
            return baseResult;
        }
    }
}
