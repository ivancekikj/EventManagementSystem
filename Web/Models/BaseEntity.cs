using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }
    }
}
