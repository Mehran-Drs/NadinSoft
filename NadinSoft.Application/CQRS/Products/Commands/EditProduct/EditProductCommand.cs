using MediatR;
using NadinSoft.Application.Common;
using System.Text.Json.Serialization;

namespace NadinSoft.Application.CQRS.Products.Commands.EditProduct
{
    public sealed class EditProductCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public DateTime ProduceDate { get; set; }
        public bool IsAvailable { get; set; }

        [JsonIgnore]
        public int CreatorId { get; set; }
    }
}
