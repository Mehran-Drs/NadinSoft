
using Microsoft.AspNetCore.Identity;

namespace NadinSoft.Domain.Entities.Products
{
    public class Product : IBaseEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public DateTime ProduceDate { get; set; }
        public bool IsAvailable { get; set; }

        public int UserId { get; set; }
        public IdentityUser User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? RemovedAt { get; set; }
    }
}
