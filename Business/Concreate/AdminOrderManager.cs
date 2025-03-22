using Business.Abstract;
using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Concreate
{
    public class AdminOrderManager : IAdminOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IOrderLineDal _orderLineDal;

        public AdminOrderManager(IOrderDal orderDal, IOrderLineDal orderLineDal)
        {
            _orderDal = orderDal;
            _orderLineDal = orderLineDal;
        }

        public List<Order> GetAllOrders(Expression<Func<Order, bool>> filter = null)
        {
            return _orderDal.GetAll(filter);
        }

        public Order GetOrderById(int id)
        {
            return _orderDal.Get(id);
        }

        public void AddOrder(Order order)
        {
            _orderDal.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            _orderDal.Update(order);
        }

        public void DeleteOrder(Order order)
        {
            // Siparişin silinmesi sırasında ilgili OrderLine kayıtlarıyla ilgili ek işlemler gerekiyorsa burada yapabilirsiniz.
            _orderDal.Delete(order);
        }
    }
}
