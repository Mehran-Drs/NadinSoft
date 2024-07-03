using MediatR;

namespace NadinSoft.Application.CQRS.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public DateTime ProduceDate { get; set; }
        public bool IsAvailable { get; set; }

        public int CreatorId { get; set; }
    }
}
