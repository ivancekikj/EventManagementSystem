﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class PurchasedTicket : Ticket
    {
        [Required]
        public int? Quantity { get; set; }
        public Guid? OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
