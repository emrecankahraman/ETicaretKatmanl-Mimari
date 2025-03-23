using Business.Abstract;
using Business.Dto;
using DataAccess.Abstract;
using Entities.Entities;
using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concreate
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IOrderLineDal _orderLineDal;

        public OrderManager(IOrderDal orderDal, IOrderLineDal orderLineDal)
        {
            _orderDal = orderDal;
            _orderLineDal = orderLineDal;
        }

        public void CreateOrder(List<CartItemDto> cartItems, OrderDto orderDetails)
        {
            if (cartItems == null || cartItems.Count == 0)
                throw new ArgumentException("Cart cannot be empty");

            var guid = Guid.NewGuid().ToString("N");
            var order = new Order
            {
                OrderNumber = guid,
                Total = cartItems.Sum(i => i.ProductPrice * i.Quantity),
                OrderDate = DateTime.Now,
                orderState = EnumOrderState.Waiting,
                UserName = orderDetails.UserName,
                Adress = orderDetails.Address,
                AdressTitle = orderDetails.AddressTitle,
                City = orderDetails.City,
                OrderLines = new List<Entities.Entities.OrderLineManager>()
            };

            _orderDal.Add(order);

            foreach (var item in cartItems)
            {
                var orderLine = new Entities.Entities.OrderLineManager
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.ProductPrice * item.Quantity
                };

                _orderLineDal.Add(orderLine);
            }
        }
    }
}
