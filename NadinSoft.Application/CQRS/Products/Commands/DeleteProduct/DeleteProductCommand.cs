using MediatR;

namespace NadinSoft.Application.CQRS.Products.Commands.EditProduct
{
    public sealed class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
    }
}
