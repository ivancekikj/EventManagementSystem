using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Event
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Location { get; set; }
        public ICollection <Schedule>? Schedules { get; set; }
    }
}
