using AutoMapper;
using MediatR;
using NadinSoft.Common;
using NadinSoft.Common.DTOs;
using NadinSoft.Common.Extensions;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Queries.GetProductsList
{
    internal sealed class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, PaginationDto<GetProductsListResult>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public GetProductsListQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PaginationDto<GetProductsListResult>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            IQueryable products;

            if (string.IsNullOrEmpty(request.CreatorFullName))
                products = _repository.AsQueryable(x => x.Creator.ToString().Contains(request.CreatorFullName), x => x.Creator);
            else
                products = _repository.AsQueryable();

            var productResult = _mapper.ProjectTo<GetProductsListResult>(products);

            return await productResult.GetPaged(request.Page, request.Limit);
        }
    }
}
