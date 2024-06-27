using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Ticket
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int? Price { get; set; }
        [Required]
        public float? Discount { get; set; }
        public Guid? ScheduleId {  get; set; }

        public Schedule? Schedule {  get; set; }

        

    }
}
