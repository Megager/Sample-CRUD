using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BD.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders(string userId);
    }
}
