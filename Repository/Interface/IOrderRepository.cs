﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order? GetById(Guid id);
        List<Order> GetAllByUserId(string userId);
        List<Order> GetAllWithExtraData();
    }
}
