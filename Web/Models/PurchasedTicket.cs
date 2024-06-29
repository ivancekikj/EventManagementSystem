using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PurchasedTicket : Ticket
    {
        [Required]
        public int? Quantity { get; set; }
        public Guid? OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
