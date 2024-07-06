using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class OrderDto
    {
        public Order Order { get; set; }
        public float TotalPrice { get; set; }
    }
}
