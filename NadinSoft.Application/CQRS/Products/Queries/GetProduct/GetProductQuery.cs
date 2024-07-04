using MediatR;
using NadinSoft.Application.Common;

namespace NadinSoft.Application.CQRS.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<Result<GetProductQueryResult>>
    {
        public int ProductId { get; set; }
    }
}
