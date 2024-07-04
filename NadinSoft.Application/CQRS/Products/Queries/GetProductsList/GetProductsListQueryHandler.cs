using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Application.Common;
using NadinSoft.Common.DTOs;
using NadinSoft.Common.Extensions;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Repositories;

namespace NadinSoft.Application.CQRS.Products.Queries.GetProductsList
{
    public sealed class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, Result<PaginationDto<GetProductsListResult>>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public GetProductsListQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<PaginationDto<GetProductsListResult>>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> products = _repository.AsQueryable();

            if (!string.IsNullOrEmpty(request.FirstName))
                products = products.Where(x => x.Creator.FirstName.Contains(request.FirstName)).Include(x => x.Creator);
            else if (!string.IsNullOrEmpty(request.LastName))
                products = products.Where(x => x.Creator.LastName.Contains(request.LastName)).Include(x => x.Creator);

            var productResult = _mapper.ProjectTo<GetProductsListResult>(products);

            var pagedResult = productResult.GetPaged(request.Page, request.Limit);

            var baseResult = new Result<PaginationDto<GetProductsListResult>>(pagedResult);

            return baseResult;
        }
    }
}
