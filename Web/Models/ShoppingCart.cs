namespace Web.Models
{
    public class ShoppingCart : BaseEntity
    {
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<TicketInCart>? TicketInCarts { get; set; }
    }
}
