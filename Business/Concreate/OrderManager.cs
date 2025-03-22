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

            // First save the order to get its ID
            _orderDal.Add(order);

            // Now create and save each OrderLine with the Order's ID
            foreach (var item in cartItems)
            {
                var orderLine = new Entities.Entities.OrderLineManager
                {
                    OrderId = order.Id, // Use the ID from the saved order
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.ProductPrice * item.Quantity
                };

                // You'll need to add this interface and implementation
                _orderLineDal.Add(orderLine);
            }
        }
    }
}
