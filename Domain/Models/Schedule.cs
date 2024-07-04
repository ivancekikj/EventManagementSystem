using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Schedule : BaseEntity
    {
        [Required]
        public DateTime? StartTime { get; set; }
        [Required]
        public DateTime? EndTime { get; set; }
        public Guid? EventId { get; set; }
        public Event? Event {  get; set; }

        public ICollection<Ticket>? Tickets { get; set; }
    }

}
