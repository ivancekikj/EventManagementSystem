using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }
    }
}
