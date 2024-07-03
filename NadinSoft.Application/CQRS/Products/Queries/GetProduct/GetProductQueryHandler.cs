using AutoMapper;
using MediatR;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Queries.GetProduct
{
    internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        public GetProductQueryHandler(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<GetProductQueryResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId);

            var mapedProduct = _mapper.Map<GetProductQueryResult>(product);

            return mapedProduct;
        }
    }
}
