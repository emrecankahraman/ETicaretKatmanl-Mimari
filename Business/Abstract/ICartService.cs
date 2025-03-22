using Business.Dto;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartService
    {
        List<CartItemDto> GetCart(string sessionKey, HttpContext httpContext);
        void SaveCart(string sessionKey, List<CartItemDto> cartItems, HttpContext httpContext);
        void AddToCart(string sessionKey, int productId, HttpContext httpContext);
        void RemoveFromCart(string sessionKey, int productId, HttpContext httpContext);
        decimal CalculateCartTotal(List<CartItemDto> cartItems);
        bool IsCartValid(List<CartItemDto> cartItems);
    }
}
