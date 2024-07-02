using MediatR;

namespace NadinSoft.Application.CQRS.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<GetProductQueryResult>
    {
        public int ProductId { get; set; }
    }
}
