using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IAdminOrderService
    {
        List<Order> GetAllOrders(Expression<Func<Order, bool>> filter = null);
        Order GetOrderById(int id);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
