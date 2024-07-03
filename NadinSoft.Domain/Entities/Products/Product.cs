
using Microsoft.AspNetCore.Identity;
using NadinSoft.Domain.Entities.Users;

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

        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? RemovedAt { get; set; }
    }
}
