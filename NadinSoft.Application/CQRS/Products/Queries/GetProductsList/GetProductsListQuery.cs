using MediatR;
using NadinSoft.Common.DTOs;

namespace NadinSoft.Application.CQRS.Products.Queries.GetProductsList
{
    public class GetProductsListQuery : IRequest<PaginationDto<GetProductsListResult>>
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
    }
}
