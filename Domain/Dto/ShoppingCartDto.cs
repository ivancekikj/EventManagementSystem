using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class ShoppingCartDto
    {
        public List<TicketInCart>? Products { get; set; }
        public float TotalPrice { get; set; }
    }
}
