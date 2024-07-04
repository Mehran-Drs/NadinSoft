using AutoMapper;
using MediatR;
using NadinSoft.Application.Common;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Queries.GetProduct
{
    public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<GetProductQueryResult>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        public GetProductQueryHandler(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Result<GetProductQueryResult>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId);

            var mapedProduct = _mapper.Map<GetProductQueryResult>(product);

            var baseResult = new Result<GetProductQueryResult>(mapedProduct);

            return baseResult;
        }
    }
}
