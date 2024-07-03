using MediatR;
using System.Text.Json.Serialization;

namespace NadinSoft.Application.CQRS.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }

        [JsonIgnore]
        public int CreatorId { get; set; }
    }
}
