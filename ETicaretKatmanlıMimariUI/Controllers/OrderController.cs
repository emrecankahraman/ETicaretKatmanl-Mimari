using Business.Abstract;
using Business.Dto;
using ETicaretKatmanlıMimariUI.ViewModels;
using ETicareBitirme.Core;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretKatmanlıMimariUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private const string CartSessionKey = "Cart";

        public OrderController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
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
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // ShippingDetails -> OrderDto mapleme
                    var orderDto = new OrderDto
                    {
                        UserName = details.UserName,
                        Address = details.Adress,
                        AddressTitle = details.AdressTitle,
                        City = details.City
                    };

                    // Siparişi veritabanına kaydet
                    _orderService.CreateOrder(cartItemDtos, orderDto);

                    // Sepeti temizle
                    cartItemDtos.Clear();
                    _cartService.SaveCart(CartSessionKey, cartItemDtos, HttpContext);

                    // Başarılı olursa bir "OrderSuccess" sayfasına yönlendirebilirsiniz
                    return RedirectToAction("OrderSuccess");
                }
                catch (Exception ex)
                {
                    // Loglama vs.
                    ModelState.AddModelError("", "Sipariş işlenirken bir hata oluştu.");
                }
            }

            // ModelState geçerli değilse formu tekrar göster
            return View(details);
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
