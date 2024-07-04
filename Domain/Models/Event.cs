using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Event : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Location { get; set; }
        public ICollection <Schedule>? Schedules { get; set; }
    }
}
