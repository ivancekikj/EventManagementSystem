using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        ShoppingCart? GetByUserId(string userId);
    }
}
