namespace NadinSoft.Application.CQRS.Products.Queries.GetProductsList
{
    public class GetProductsListResult
    {
        public string Name { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public DateTime ProduceDate { get; set; }
        public bool IsAvailable { get; set; }

        public string CreatorFullName { get; set; }
    }
}
