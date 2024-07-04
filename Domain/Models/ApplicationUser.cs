using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
