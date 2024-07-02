
namespace NadinSoft.Domain.Entities.Products
{
    public class Product : IBaseEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public DateTime ProductDate { get; set; }
        public bool IsAvailable { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? RemovedAt { get; set; }
    }
}
