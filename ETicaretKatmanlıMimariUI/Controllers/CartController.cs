using Business.Abstract;
using Business.Dto;
using Entities.Entities;
using ETicareBitirme.Core;
using ETicaretKatmanlıMimariUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretKatmanlıMimariUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private const string CartSessionKey = "Cart";

        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var cartItemDtos = _cartService.GetCart(CartSessionKey, HttpContext);

            if (cartItemDtos == null || cartItemDtos.Count == 0)
            {
                // Boş bir liste gönder
                return View(new List<CardItem>());
            }

            // Map DTOs to view models
            var cartItems = cartItemDtos.Select(dto => new CardItem
            {
                Product = new Product
                {
                    Id = dto.ProductId,
                    Name = dto.ProductName,
                    Price = dto.ProductPrice,
                    Image = dto.ProductImage
                },
                Quantity = dto.Quantity
            }).ToList();

            ViewBag.Total = _cartService.CalculateCartTotal(cartItemDtos);
            SessionHelper.Count = cartItems.Count;
            return View(cartItems);
        }

        public IActionResult Buy(int id)
        {
            _cartService.AddToCart(CartSessionKey, id, HttpContext);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _cartService.RemoveFromCart(CartSessionKey, id, HttpContext);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CheckOut()
        {
            var cartItemDtos = _cartService.GetCart(CartSessionKey, HttpContext);
            if (!_cartService.IsCartValid(cartItemDtos))
            {
                ModelState.AddModelError("Ürün yok", "Sepetinizde ürün bulunmamaktadır.");
                return RedirectToAction("Index", "Home");
            }

            return View(new ShippingDetails());
        }

        [HttpPost]
        public IActionResult CheckOut(ShippingDetails details)
        {
            var cartItemDtos = _cartService.GetCart(CartSessionKey, HttpContext);
            if (!_cartService.IsCartValid(cartItemDtos))
            {
                ModelState.AddModelError("Ürün yok", "Sepetinizde ürün bulunmamaktadır.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Map ShippingDetails to OrderDto
                    var orderDto = new OrderDto
                    {
                        UserName = details.UserName,
                        Address = details.Adress,
                        AddressTitle = details.AdressTitle,
                        City = details.City
                    };

                    _orderService.CreateOrder(cartItemDtos, orderDto);

                     cartItemDtos.Clear();
                    _cartService.SaveCart(CartSessionKey, cartItemDtos, HttpContext);
                }
                catch (Exception ex)
                {
                    // Log exception and handle error
                    ModelState.AddModelError("", "Sipariş işlenirken bir hata oluştu.");
                }
            }

            return View();
        }
    }
}


