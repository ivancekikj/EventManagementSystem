using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TicketInCart : BaseEntity
    {
        [Required]
        public int? Quantity { get; set; }
        public Guid? TicketForPurchaseId { get; set; }
        public virtual TicketForPurchase? TicketForPurchase { get; set; }
        public Guid? ShoppingCartId { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
    }
}
