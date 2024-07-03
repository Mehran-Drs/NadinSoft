using MediatR;

namespace NadinSoft.Application.CQRS.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
    }
}
