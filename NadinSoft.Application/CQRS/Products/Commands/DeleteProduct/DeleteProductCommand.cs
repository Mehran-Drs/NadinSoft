using MediatR;
using NadinSoft.Application.Common;
using System.Text.Json.Serialization;

namespace NadinSoft.Application.CQRS.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommand : IRequest<Result<bool>>
    {
        public int ProductId { get; set; }

        [JsonIgnore]
        public int CreatorId { get; set; }
    }
}
