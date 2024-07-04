using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Ticket : BaseEntity
    {
        [Required]
        public int? Price { get; set; }
        [Required]
        public float? Discount { get; set; }
        public Guid? ScheduleId {  get; set; }
        public Schedule? Schedule {  get; set; }
    }
}
