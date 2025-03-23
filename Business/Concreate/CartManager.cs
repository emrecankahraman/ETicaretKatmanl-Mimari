using Business.Abstract;
using Business.Dto;
using DataAccess.Abstract;
using Entities.Entities;
using ETicareBitirme.Core;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Business.Concreate
{
    public class CartManager : ICartService
    {

        private readonly IProductService _productService;

        public CartManager(IProductService productService)
        {
            _productService = productService;
        }

        public List<CartItemDto> GetCart(string sessionKey, HttpContext httpContext)
        {
            return SessionHelper.GetObjectFromJson<List<CartItemDto>>(httpContext.Session, sessionKey);
        }

        public void SaveCart(string sessionKey, List<CartItemDto> cartItems, HttpContext httpContext)
        {
            SessionHelper.SetObjectAsJson(httpContext.Session, sessionKey, cartItems);
        }

        public void AddToCart(string sessionKey, int productId, HttpContext httpContext)
        {
            var cart = GetCart(sessionKey, httpContext);

            if (cart == null)
            {
                cart = new List<CartItemDto>();

                // Get product from product service
                var product = _productService.GetById(productId);

                cart.Add(new CartItemDto
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    ProductImage = product.Image,
                    Quantity = 1
                });
            }
            else
            {
                int index = FindProductInCart(cart, productId);
                if (index < 0)
                {
                    var product = _productService.GetById(productId);

                    cart.Add(new CartItemDto
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        ProductImage = product.Image,
                        Quantity = 1
                    });
                }
                else
                {
                    cart[index].Quantity++;
                }
            }

            SaveCart(sessionKey, cart, httpContext);
        }

        public void RemoveFromCart(string sessionKey, int productId, HttpContext httpContext)
        {
            var cart = GetCart(sessionKey, httpContext);
            if (cart != null)
            {
                int index = FindProductInCart(cart, productId);
                if (index >= 0)
                {
                    if (cart[index].Quantity > 1)
                    {
                        cart[index].Quantity--;
                    }
                    else
                    {
                        cart.RemoveAt(index);
                    }

                    if (cart.Count == 0)
                    {
                        cart = new List<CartItemDto>();
                    }

                    SaveCart(sessionKey, cart, httpContext);
                }
            }
        }

        private int FindProductInCart(List<CartItemDto> cart, int productId)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == productId)
                {
                    return i;
                }
            }
            return -1;
        }

        public decimal CalculateCartTotal(List<CartItemDto> cartItems)
        {
            if (cartItems == null || cartItems.Count == 0)
                return 0;

            return cartItems.Sum(x => x.ProductPrice * x.Quantity);
        }   

        public bool IsCartValid(List<CartItemDto> cartItems)
        {
            return cartItems != null && cartItems.Count > 0;
        }

        
    }

}

