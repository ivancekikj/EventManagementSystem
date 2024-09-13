using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PartnerStoreEvent : BaseEntity
    {
        public string? EventName { get; set; }
        public string? HostName { get; set; }
        public string? ImageUrl { get; set; }
        public List<string>? Schedules { get; set; }
    }
}
