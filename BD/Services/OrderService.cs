using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BD.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BD.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;

        public OrderService(IBaseRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> GetOrders(string userId)
        {
            return _orderRepository.Get(x => x.LoginId == userId).AsEnumerable();
        }
    }
}