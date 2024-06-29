using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Order : BaseEntity
    {
        [Required]
        public DateTime? DateTimeOrdered { get; set; }
        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<PurchasedTicket>? PurchasedTickets { get; set; }
    }
}
