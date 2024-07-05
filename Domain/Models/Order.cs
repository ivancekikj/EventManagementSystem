using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Order : BaseEntity
    {
        [Required]
        public DateTime? DateTimeOrdered { get; set; }
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<PurchasedTicket>? PurchasedTickets { get; set; }
    }
}
