using Microsoft.AspNetCore.Identity;

namespace e_commercee.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public Address Address { get; set; }
        public string Status { get; set; }
        public int AddressId { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt  { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

    }
}
