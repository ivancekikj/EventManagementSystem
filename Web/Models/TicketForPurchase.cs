﻿namespace Web.Models
{
    public class TicketForPurchase : Ticket
    {
        public virtual ICollection<TicketInCart>? TicketInCarts { get; set; }
    }
}
