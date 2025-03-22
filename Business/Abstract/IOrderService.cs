using Business.Dto;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
       
            void CreateOrder(List<CartItemDto> cartItems, OrderDto orderDetails);
        
    }
}
